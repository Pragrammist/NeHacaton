using Newtonsoft.Json;

namespace HendInRentApi.Dto.SelfInfo.Rent
{
    public class OutputDiscountDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public int Title { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }
    }
}
