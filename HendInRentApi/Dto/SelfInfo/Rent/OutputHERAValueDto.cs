using Newtonsoft.Json;

namespace HendInRentApi.Dto.SelfInfo.Rent
{
    public class OutputHERAValueDto
    {
        public int Id { get; set; }
        public string Period { get; set; } = null!;
        public string Value { get; set; } = null!;
        [JsonProperty("more_then")]
        public string Morethen { get; set; } = null!;
        [JsonProperty("is_fixed")]
        public bool IsFixed { get; set; }
    }
}
