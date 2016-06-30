using System.Collections.Generic;
using Newtonsoft.Json;

namespace skadisteam.login.Models.Json
{
    public class DoLoginResponse
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
        [JsonProperty(PropertyName = "requires_twofactor")]
        public bool RequiresTwoFactor { get; set; }
        [JsonProperty(PropertyName = "login_complete")]
        public bool LoginComplete { get; set; }
        [JsonProperty(PropertyName = "transfer_urls")]
        public List<string> TransferUrls { get; set; }
        [JsonProperty(PropertyName = "transfer_parameters")]
        public TransferParameters TransferParameters { get; set; }
    }
}
