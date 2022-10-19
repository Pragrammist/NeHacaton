using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Web.Dtos.Sales.Inventory
{
    public class InputDiscountDto
    {
        [JsonProperty("id")]        
        public int? Id { get; set; }

        [JsonProperty("title")]        
        public int? Title { get; set; }

        [JsonProperty("price")]        
        public int? Price { get; set; }
    } 
}
