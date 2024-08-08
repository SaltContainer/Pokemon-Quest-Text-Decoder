using Newtonsoft.Json;

namespace Pokemon_Quest_Text_Decoder.Data
{
    public class MessageLabelDataSet
    {
        [JsonProperty(PropertyName = "m_datas")]
        public IList<MessageLabel> Data { get; set; }
    }
}
