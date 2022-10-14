using Newtonsoft.Json;

namespace HendInRentApi
{

    

    public class OutputPriceDto
    {
        public string Title { get; set; }
        [JsonProperty("price_logic_id")]
        public string PriceLogicId { get; set; }
        [JsonProperty("point_id")]
        public int PointId { get; set; }
    }
    

}
