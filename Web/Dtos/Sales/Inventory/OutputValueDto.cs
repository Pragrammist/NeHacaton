using Newtonsoft.Json;

namespace Web.Dtos.Sales.Inventory
{
    public class OutputValueDto
    {
        public int Id { get; set; }
        public string Period { get; set; }
        public string Value { get; set; }
        [JsonProperty("more_then")]
        public string MoreThen { get; set; }
        [JsonProperty("is_fixed")]
        public bool IsFixed { get; set; }
    }
}
