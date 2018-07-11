﻿using System.Collections.Generic;
using System.IO;

namespace mPqTextAssetDecoder
{
    class MessageData
    {
        public enum Coded
        {
            DATA_CODED,
            DATA_NO_CODED
        }

        public struct MessageDataHeader
        {
            public ushort numLangs;         // now is always 1
            public ushort numStrings;
            public uint maxLangBlockSize;
            public Coded reserved;
            public List<uint> ofsLangBlocks;
        }

        public struct MessageStringParameterBlock
        {
            public uint offset;
            public ushort len;
            public ushort userParam;
        }

        public struct MessageLauguageBlock
        {
            public uint size;
            public List<MessageStringParameterBlock> parameters;
        }
        
        public MessageDataHeader header;
        public MessageLauguageBlock block;
        public List<string> messages;

        public void Decode(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            using(BinaryReader br = new BinaryReader(ms))
            {
                header.numLangs = br.ReadUInt16();
                header.numStrings = br.ReadUInt16();
                header.maxLangBlockSize = br.ReadUInt32();
                header.reserved = (Coded)br.ReadUInt32();
                header.ofsLangBlocks = new List<uint>();
                for (int i = 0; i < header.numLangs; i++)
                {
                    header.ofsLangBlocks.Add(br.ReadUInt32());
                }
                block.parameters = new List<MessageStringParameterBlock>();
                block.size = br.ReadUInt32();
                for (int i = 0; i < header.numStrings; i++)
                {
                    MessageStringParameterBlock mspb;
                    mspb.offset = br.ReadUInt32();
                    mspb.len = br.ReadUInt16();
                    mspb.userParam = br.ReadUInt16();
                    block.parameters.Add(mspb);
                }

                messages = new List<string>();
                for (int i = 0; i < block.parameters.Count; i++)
                {
                    var mspb = block.parameters[i];
                    var ms_offset = mspb.offset + header.ofsLangBlocks[0];
                    ms.Seek(ms_offset, SeekOrigin.Begin);
                    if (Coded.DATA_CODED == header.reserved)
                    {
                        var key = (ushort)(10627 * i + 31881);
                        var msg_decode = "";
                        var msg_len = mspb.len;
                        for (int j = 0; j < msg_len; j++)
                        {
                            msg_decode += (char)(br.ReadUInt16() ^ key);
                            key = (ushort)((ushort)(key >> 13) | 8 * key);
                        }
                        messages.Add(msg_decode);
                    }
                    else
                    {
                        var msg_decode = "";
                        var msg_len = mspb.len;
                        for (int j = 0; j < msg_len; j++)
                        {
                            msg_decode += (char)(br.ReadUInt16());
                        }
                        messages.Add(msg_decode);
                    }
                }

            }
        }


    }
}
