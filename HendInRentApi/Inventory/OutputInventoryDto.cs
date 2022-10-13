using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace HendInRentApi
{

   

    public class OutputInventoryDto
    {
        public int Id { get; set; }
        [JsonProperty("parent_id")]
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [JsonProperty("rent_number")]
        public int RentNumber { get; set; }
        [JsonProperty("state_id")]
        public int StateId { get; set; }
        [JsonProperty("buy_price")]
        public int BuyPrice { get; set; }
        [JsonProperty("buy_date")]
        public DateTime BuyDate { get; set; }
        public string Option { get; set; }
        public OutPutStateDto State { get; set; } = null!;
        public OutputPointDto Point { get; set; } = null!;
        public OutputPriceDto Price { get; set; } = null!;
        public OutputDiscountsDto Discounts { get; set; } = null!;
    }
    

}
