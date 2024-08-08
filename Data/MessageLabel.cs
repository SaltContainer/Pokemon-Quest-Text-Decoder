using Newtonsoft.Json;

namespace Pokemon_Quest_Text_Decoder.Data
{
    public class MessageLabel
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "FileName")]
        public string FileName { get; set; }

        [JsonProperty(PropertyName = "DataPath")]
        public long DataPath { get; set; }

        [JsonProperty(PropertyName = "Label")]
        public int Index { get; set; }
    }
}
