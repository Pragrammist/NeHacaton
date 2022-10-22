using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HendInRentApi.Dto.Inventory
{
    public class InputHIRADiscountsDto
    {
        public InputHIRADiscountDto? Discount { get; set; }
        
        [JsonProperty("discount_id")]
        public int? DiscountId { get; set; }
        
        [JsonProperty("resource_id")]
        public int? ResourceId { get; set; }
    }
}
