using Newtonsoft.Json;

namespace QuestTextEditor.Data
{
    public class MessageLabelDataSet
    {
        [JsonProperty(PropertyName = "m_datas")]
        public IList<MessageLabel> Data { get; set; }

        public IList<MessageLabel> this[string fileName] { get => Data.Where(d => d.FileName == fileName).OrderBy(d => d.Index).ToList(); }
    }
}
