namespace Pokemon_Quest_Text_Decoder.Data
{
    public class MessageData
    {
        private EncodedMessageData encodedData;

        public EncodedMessageData.Coded EncodingMethod { get => encodedData.header.reserved; set => encodedData.header.reserved = value; }
        public ushort LanguageCount { get => encodedData.header.numLangs; }
        public ushort LabelCount { get => encodedData.header.numStrings; }

        public List<string> this[int lang] { get => encodedData.blocks[lang].messages; set => encodedData.blocks[lang].messages = value; }
        public string this[int lang, int labelIndex] { get => encodedData.blocks[lang].messages[labelIndex]; set => encodedData.blocks[lang].messages[labelIndex] = value; }

        public MessageData(byte[] rawData)
        {
            encodedData = new EncodedMessageData();
            encodedData.ReadFromBytes(rawData);
        }

        public List<EncodedMessageData.MessageStringParameterBlock> GetStringParams(int lang)
        {
            return encodedData.blocks[lang].parameters;
        }

        public void SetStringParams(int lang, List<EncodedMessageData.MessageStringParameterBlock> parameters)
        {
            encodedData.blocks[lang].parameters = parameters;
        }

        public string ExportAllText()
        {
            return encodedData.ExportText();
        }

        public byte[] ConvertToBytes()
        {
            return encodedData.ConvertToBytes(EncodedMessageData.Coded.DATA_NO_CODED);
        }
    }
}
