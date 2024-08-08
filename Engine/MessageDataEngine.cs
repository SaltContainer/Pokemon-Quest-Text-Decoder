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

        public MessageLabelDataSet ReadMessageLabelDataSetFromJSONFile(string path)
        {
            return JsonConvert.DeserializeObject<MessageLabelDataSet>(File.ReadAllText(path));
        }

        public void SaveMessageDataToBinFile(string path, MessageData data)
        {
            File.WriteAllBytes(path, data.ConvertToBytes());
        }

        public void SaveMessageDataToTextFile(string path, MessageData data)
        {
            File.WriteAllText(path, data.ExportAllText());
        }
    }
}
