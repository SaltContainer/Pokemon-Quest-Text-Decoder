using Newtonsoft.Json;
using QuestTextEditor.Data;

namespace QuestTextEditor.Engine
{
    public class MessageDataEngine
    {
        public MessageData ReadMessageDataFromBinFile(string path)
        {
            return new MessageData(File.ReadAllBytes(path));
        }

        public MessageData ReadMessageDataFromCSVFile(string path)
        {
            // TODO: Read CSV
            // Method on MessageData that changes the data directly?
            return new MessageData(Array.Empty<byte>());
        }

        public MessageLabelDataSet ReadMessageLabelDataSetFromJSONFile(string path)
        {
            return JsonConvert.DeserializeObject<MessageLabelDataSet>(File.ReadAllText(path));
        }

        public void SaveMessageDataToBinFile(string path, MessageData data)
        {
            File.WriteAllBytes(path, data.ConvertToBytes());
        }

        public void SaveMessageDataToTextFile(string path, MessageData data, int lang)
        {
            File.WriteAllText(path, data.ExportAllText(lang));
        }

        public void SaveMessageDataToCSVFile(string path, MessageData data, int lang, List<string> labelNames)
        {
            File.WriteAllText(path, data.ExportAsCSV(lang, labelNames));
        }
    }
}
