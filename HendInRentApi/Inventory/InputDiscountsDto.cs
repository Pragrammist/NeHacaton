using Newtonsoft.Json;

namespace HendInRentApi
{

    

    public class InputDiscountsDto
    {
        public InputDiscountDto? Discount { get; set; }
        [JsonProperty("discount_id")]
        public int? DiscountId { get; set; }
        [JsonProperty("resource_id")]
        public int? ResourceId { get; set; }
    }
    

}
