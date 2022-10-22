using Newtonsoft.Json;

namespace HendInRentApi.Dto.Inventory
{
    public class OutputHERAValueDto
    {
        public int Id { get; set; }
        public string Period { get; set; } = null!;
        public int Value { get; set; }
        [JsonProperty("more_then")]
        public string MoreThen { get; set; } = null!;
        [JsonProperty("is_fixed")]
        public bool IsFixed { get; set; }
    }
}
