using Newtonsoft.Json;

namespace skadisteam.login.Models
{
    internal class OffsetResponse
    {
        [JsonProperty(PropertyName = "response")]
        internal OffsetParameters OffsetParameters { get; set; }
    }
}
