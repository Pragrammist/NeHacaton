using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HendInRentApi.Dto.Inventory
{
    public class InputHIRADiscountDto
    {
        [JsonProperty("id")]        
        public int? Id { get; set; }

        [JsonProperty("title")]        
        public int? Title { get; set; }

        [JsonProperty("price")]        
        public int? Price { get; set; }
    } 
}
