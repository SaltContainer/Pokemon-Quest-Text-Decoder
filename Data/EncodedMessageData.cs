using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;

namespace QuestTextEditor.Data
{
    public class EncodedMessageData
    {
        public enum Coded : uint
        {
            DATA_CODED = 0,
            DATA_NO_CODED = 1,
        }

        public class MessageDataHeader
        {
            public ushort numLangs;
            public ushort numStrings;
            public uint maxLangBlockSize;
            public Coded reserved;
            public List<uint> ofsLangBlocks;

            public uint ByteSize => sizeof(ushort) + sizeof(ushort) + sizeof(uint) + sizeof(Coded) + ((uint)ofsLangBlocks.Count * sizeof(uint));

            public MessageDataHeader()
            {
                ofsLangBlocks = new List<uint>();
            }
        }

        public class MessageStringParameterBlock
        {
            public uint offset;
            public ushort len;
            public ushort userParam;

            public static uint ByteSize => sizeof(uint) + sizeof(ushort) + sizeof(ushort);
        }

        public class MessageLanguageBlock
        {
            public uint size;
            public List<MessageStringParameterBlock> parameters;
            public List<string> messages;
            public List<byte[]> encodedMessages;

            public MessageLanguageBlock()
            {
                parameters = new List<MessageStringParameterBlock>();
                messages = new List<string>();
                encodedMessages = new List<byte[]>();
            }
        }

        public MessageDataHeader header;
        public List<MessageLanguageBlock> blocks;

        public EncodedMessageData()
        {
            header = new MessageDataHeader();
            blocks = new List<MessageLanguageBlock>();
        }

        public void ReadFromBytes(byte[] data)
        {
            ClearData();

            using (MemoryStream ms = new MemoryStream(data))
            using (BinaryReader br = new BinaryReader(ms))
            {
                header.numLangs = br.ReadUInt16();
                header.numStrings = br.ReadUInt16();
                header.maxLangBlockSize = br.ReadUInt32();
                header.reserved = (Coded)br.ReadUInt32();
                header.ofsLangBlocks = new List<uint>();

                for (int i=0; i<header.numLangs; i++)
                    header.ofsLangBlocks.Add(br.ReadUInt32());

                for (int i=0; i<header.numLangs; i++)
                    blocks.Add(new MessageLanguageBlock());

                for (int i=0; i<header.numLangs; i++)
                {
                    ms.Seek(header.ofsLangBlocks[i], SeekOrigin.Begin);

                    blocks[i].size = br.ReadUInt32();
                    for (int j=0; j<header.numStrings; j++)
                    {
                        MessageStringParameterBlock mspb = new MessageStringParameterBlock();
                        mspb.offset = br.ReadUInt32();
                        mspb.len = br.ReadUInt16();
                        mspb.userParam = br.ReadUInt16();
                        blocks[i].parameters.Add(mspb);
                    }

                    for (int j=0; j<blocks[i].parameters.Count; j++)
                    {
                        var mspb = blocks[i].parameters[j];
                        var ms_offset = mspb.offset + header.ofsLangBlocks[i];
                        ms.Seek(ms_offset, SeekOrigin.Begin);

                        byte[] bytes = new byte[mspb.len*2];
                        for (int k=0; k<bytes.Length; k++)
                            bytes[k] = br.ReadByte();

                        blocks[i].encodedMessages.Add(bytes);
                        blocks[i].messages.Add(DecodeString(bytes, header.reserved, (ushort)(10627 * j + 31881)));
                    }
                }
            }
        }

        public byte[] ConvertToBytes()
        {
            RegenerateMetadata(header.reserved);

            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                bw.Write(header.numLangs);
                bw.Write(header.numStrings);
                bw.Write(header.maxLangBlockSize);
                bw.Write((uint)header.reserved);

                for (int i=0; i<header.ofsLangBlocks.Count; i++)
                    bw.Write(header.ofsLangBlocks[i]);

                for (int i=0; i<blocks.Count; i++)
                {
                    bw.Write(blocks[i].size);
                    for (int j=0; j<blocks[i].parameters.Count; j++)
                    {
                        bw.Write(blocks[i].parameters[j].offset);
                        bw.Write(blocks[i].parameters[j].len);
                        bw.Write(blocks[i].parameters[j].userParam);
                    }

                    for (int j=0; j<blocks[i].encodedMessages.Count; j++)
                    {
                        bw.Write(blocks[i].encodedMessages[j]);
                    }
                }

                return ms.ToArray();
            }
        }

        public string ExportText(int lang)
        {
            StringBuilder sb = new StringBuilder();
            for (int i=0; i<blocks[lang].messages.Count; i++)
                sb.AppendLine(blocks[lang].messages[i]);

            return sb.ToString();
        }

        public string ExportAsCSV(int lang, List<string> labelNames)
        {
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);
            using var reader = new StreamReader(stream);
            using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = false });

            csv.WriteRecords(
                labelNames.Zip(blocks[lang].messages,
                    (n, m) => (n, m))
                .Zip(blocks[lang].parameters.Select(p => p.userParam),
                    (nm, p) => new CSVLabel { LabelName = nm.n, Label = nm.m, UserParam = p }));

            writer.Flush();

            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            return reader.ReadToEnd();
        }

        public void ImportFromCSV(int lang, List<CSVLabel> data, List<string> labelNames)
        {
            for (int i=0; i<labelNames.Count; i++)
            {
                var labelName = labelNames[i];
                var csvLabel = data.Find(l => l.LabelName == labelName);
                if (csvLabel != null)
                {
                    blocks[lang].messages[i] = csvLabel.Label;
                    blocks[lang].parameters[i].userParam = csvLabel.UserParam;
                }
            }
        }

        private string DecodeString(byte[] rawData, Coded mode, ushort key)
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryReader br = new BinaryReader(ms))
            {
                switch (mode)
                {
                    case Coded.DATA_CODED:
                    {
                        var msg_decode = "";
                        for (int i=0; i<rawData.Length/2; i++)
                        {
                            ushort chara = (ushort)((rawData[i*2+1] * 256) + rawData[i*2]);
                            msg_decode += (char)(chara ^ key);
                            key = (ushort)((ushort)(key >> 13) | 8 * key);
                        }
                        return msg_decode;
                    }

                    case Coded.DATA_NO_CODED:
                    {
                        var msg_decode = "";
                        for (int i=0; i<rawData.Length/2; i++)
                        {
                            ushort chara = (ushort)((rawData[i*2+1] * 256) + rawData[i*2]);
                            msg_decode += (char)chara;
                        }
                        return msg_decode;
                    }

                    default:
                        return string.Empty;
                }
            }
        }

        private byte[] EncodeString(Coded mode, string str, ushort key)
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                switch (mode)
                {
                    case Coded.DATA_NO_CODED:
                    {
                        for (int i=0; i<str.Length; i++)
                            bw.Write(BitConverter.GetBytes(str[i]));

                        bw.Write((ushort)0);

                        return ms.ToArray();
                    }

                    case Coded.DATA_CODED:
                        for (int i=0; i<str.Length; i++)
                        {
                            ushort chara = str[i];
                            chara ^= key;
                            bw.Write(chara);

                            key = (ushort)((ushort)(key >> 13) | 8 * key);
                        }

                        bw.Write((ushort)0);

                        return ms.ToArray();

                    default:
                        return Array.Empty<byte>();
                }
            }
        }

        private void RegenerateMetadata()
        {
            header.numLangs = (ushort)blocks.Count;
            header.numStrings = (ushort)blocks[0].messages.Count;

            for (int i=0; i<blocks.Count; i++)
            {
                blocks[i].size = sizeof(uint) + ((uint)blocks[i].parameters.Count * MessageStringParameterBlock.ByteSize);
                for (int j=0; j<blocks[i].messages.Count; j++)
                {
                    blocks[i].parameters[j].offset = blocks[i].size;

                    var bytes = EncodeString(header.reserved, blocks[i].messages[j], (ushort)(10627 * j + 31881));
                    blocks[i].encodedMessages[j] = bytes;

                    blocks[i].parameters[j].len = (ushort)((bytes.Length - 2) / 2);

                    blocks[i].size += (uint)bytes.Length;
                }
            }

            header.maxLangBlockSize = blocks.Max(b => b.size);

            uint currentBlockOffset = header.ByteSize;
            for (int i=0; i<blocks.Count; i++)
            {
                header.ofsLangBlocks[i] = currentBlockOffset;
                currentBlockOffset += blocks[i].size;
            }
        }

        private void ClearData()
        {
            header.numLangs = 0;
            header.numStrings = 0;
            header.maxLangBlockSize = 0;
            header.reserved = Coded.DATA_CODED;
            header.ofsLangBlocks.Clear();

            blocks.Clear();
        }
    }
}
