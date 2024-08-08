using Newtonsoft.Json;

namespace QuestTextEditor.Data
{
    public class MessageLabelDataSet
    {
        [JsonProperty(PropertyName = "m_datas")]
        public IList<MessageLabel> Data { get; set; }
    }
}
