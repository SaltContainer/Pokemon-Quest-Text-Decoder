using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using QuestTextEditor.Data;
using System.Globalization;

namespace QuestTextEditor.Engine
{
    public class MessageDataEngine
    {
        public MessageData ReadMessageDataFromBinFile(string path)
        {
            return new MessageData(File.ReadAllBytes(path));
        }

        public void ReadCSVFileIntoMessageData(string path, MessageData data, int lang)
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = false });
            data.ImportFromCSV(lang, csv.GetRecords<CSVLabel>().ToList());
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

        public void SaveMessageDataToCSVFile(string path, MessageData data, int lang)
        {
            File.WriteAllText(path, data.ExportAsCSV(lang));
        }
    }
}
