using Newtonsoft.Json;

namespace Web.Dtos.Sales.Inventory
{

    

    public class OutputPriceDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonProperty("point_id")]
        public int PointId { get; set; }
        public string Article { get; set; }
        public List<OutputValueDto> Values { get; set; }
    }
    

}
