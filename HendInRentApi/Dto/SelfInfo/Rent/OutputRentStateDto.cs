using Newtonsoft.Json;

namespace HendInRentApi.Dto.SelfInfo.Rent
{
    public class OutputRentStateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string @Const { get; set; }
        [JsonProperty("dom_class")]
        public string DomClass { get; set; }
    }
}
