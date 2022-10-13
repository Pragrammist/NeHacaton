using Newtonsoft.Json;


namespace HendInRentApi
{

    

    public class InputInventoryDto
    {
        public string? Search { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        [JsonProperty("rent_number")]
        public string? RentNumber { get; set; }
        [JsonProperty("state_id")]
        public int? StateId { get; set; }
        [JsonProperty("limit")]
        public int? Limit { get; set; }

        public int? Offset { get; set; }
        public InputDiscountsDto? Discounts { get; set; }
    }
    

}
