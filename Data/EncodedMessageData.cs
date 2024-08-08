using System.Text;

namespace Pokemon_Quest_Text_Decoder.Data
{
    public class EncodedMessageData
    {
        public enum Coded : uint
        {
            DATA_CODED,
            DATA_NO_CODED
        }

        public class MessageDataHeader
        {
            public ushort numLangs;
            public ushort numStrings;
            public uint maxLangBlockSize;
            public Coded reserved;
            public List<uint> ofsLangBlocks;

            public uint ByteSize => sizeof(ushort) + sizeof(ushort) + sizeof(uint) + sizeof(Coded) + ((uint)ofsLangBlocks.Count * sizeof(uint));
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

                blocks[0].parameters = new List<MessageStringParameterBlock>();
                blocks[0].size = br.ReadUInt32();
                for (int i=0; i<header.numStrings; i++)
                {
                    MessageStringParameterBlock mspb = new MessageStringParameterBlock();
                    mspb.offset = br.ReadUInt32();
                    mspb.len = br.ReadUInt16();
                    mspb.userParam = br.ReadUInt16();
                    blocks[0].parameters.Add(mspb);
                }

                blocks[0].messages = new List<string>();
                blocks[0].encodedMessages = new List<byte[]>();
                for (int i=0; i<blocks[0].parameters.Count; i++)
                {
                    var mspb = blocks[0].parameters[i];
                    var ms_offset = mspb.offset + header.ofsLangBlocks[0];
                    ms.Seek(ms_offset, SeekOrigin.Begin);

                    byte[] bytes = new byte[mspb.len*2];
                    for (int j=0; j<bytes.Length; j++)
                        bytes[j] = br.ReadByte();

                    blocks[0].encodedMessages.Add(bytes);
                    blocks[0].messages.Add(DecodeString(bytes, header.reserved, (ushort)(10627 * i + 31881)));
                }
            }
        }

        public byte[] ConvertToBytes(Coded mode)
        {
            RegenerateMetadata(mode);

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

        public string ExportText()
        {
            StringBuilder sb = new StringBuilder();
            for (int i=0; i<blocks[0].messages.Count; i++)
                sb.AppendFormat("{0}\n", blocks[0].messages[i]);

            return sb.ToString();
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

        private byte[] EncodeString(Coded mode, string str)
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
                        // TODO: Reverse the encoding
                        return Array.Empty<byte>();

                    default:
                        return Array.Empty<byte>();
                }
            }
        }

        private void RegenerateMetadata(Coded mode)
        {
            header.numLangs = (ushort)blocks.Count;
            header.numStrings = (ushort)blocks[0].messages.Count;
            header.reserved = mode;

            for (int i=0; i<blocks.Count; i++)
            {
                blocks[i].size = sizeof(uint) + ((uint)blocks[i].parameters.Count * MessageStringParameterBlock.ByteSize);
                for (int j=0; j<blocks[i].messages.Count; j++)
                {
                    blocks[i].parameters[j].offset = blocks[i].size;

                    var bytes = EncodeString(mode, blocks[i].messages[j]);
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
    }
}
