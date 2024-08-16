namespace QuestTextEditor.Data
{
    public class MessageData
    {
        private EncodedMessageData encodedData;

        public EncodedMessageData.Coded EncodingMethod { get => encodedData.header.reserved; set => encodedData.header.reserved = value; }
        public ushort LanguageCount { get => encodedData.header.numLangs; }
        public ushort LabelCount { get => encodedData.header.numStrings; }
        public string FileName { get; set; }
        public List<MessageLabel> MessageLabels { get; set; }
        public List<string> LabelNames { get => MessageLabels.Select(l => l.Id).ToList(); }

        public List<string> this[int lang] { get => encodedData.blocks[lang].messages; set => encodedData.blocks[lang].messages = value; }
        public string this[int lang, int labelIndex] { get => encodedData.blocks[lang].messages[labelIndex]; set => encodedData.blocks[lang].messages[labelIndex] = value; }

        public MessageData(byte[] rawData)
        {
            encodedData = new EncodedMessageData();
            encodedData.ReadFromBytes(rawData);

            FileName = string.Empty;
            MessageLabels = new List<MessageLabel>();
        }

        public ushort GetUserParam(int lang, int labelIndex)
        {
            return encodedData.blocks[lang].parameters[labelIndex].userParam;
        }

        public void SetUserParam(int lang, int labelIndex, ushort userParam)
        {
            encodedData.blocks[lang].parameters[labelIndex].userParam = userParam;
        }

        public List<string> GetDefaultLabelRepresentations(int lang)
        {
            return encodedData.blocks[lang].messages.Select((m, i) => "[" + i + "] " + m).ToList();
        }

        public string ExportAllText(int lang)
        {
            return encodedData.ExportText(lang);
        }

        public string ExportAsCSV(int lang)
        {
            return encodedData.ExportAsCSV(lang, LabelNames);
        }

        public void ImportFromCSV(int lang, List<CSVLabel> data)
        {
            encodedData.ImportFromCSV(lang, data, LabelNames);
        }

        public byte[] ConvertToBytes()
        {
            return encodedData.ConvertToBytes(EncodedMessageData.Coded.DATA_NO_CODED);
        }

        public void SetUpLabelNames(MessageLabelDataSet dataSet)
        {
            MessageLabels.Clear();

            var labelsFromFile = dataSet.Data.Where(d => d.FileName == FileName).OrderBy(d => d.Index);
            int index = 0;
            foreach (var label in labelsFromFile)
            {
                if (label.Index == index)
                {
                    MessageLabels.Add(label);
                    index++;
                } 
            }
        }
    }
}
