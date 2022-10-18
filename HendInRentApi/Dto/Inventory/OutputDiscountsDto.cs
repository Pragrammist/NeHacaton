using Newtonsoft.Json;

namespace HendInRentApi
{
    public class OutputDiscountsDto
    {
        public OutputDiscountDto Discount { get; set; } = null!;
        
        [JsonProperty("discount_id")]
        public int DiscountId { get; set; }
        
        [JsonProperty("resource_id")]
        public int ResourceId { get; set; }
    }   
}
