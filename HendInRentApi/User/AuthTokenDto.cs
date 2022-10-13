using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HendInRentApi
{

    public class OutputAuthTokenDto
    {
        [JsonProperty("access_token")]
        
        public string AccessToken { get; set; } = null!;
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; } = null!;
    }
    

}
