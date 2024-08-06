using Pokemon_Quest_Text_Decoder.Data;

namespace Pokemon_Quest_Text_Decoder.Engine
{
    public class MessageDataEngine
    {
        public MessageData ReadMessageDataFromBinFile(string path)
        {
            return new MessageData(File.ReadAllBytes(path));
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
