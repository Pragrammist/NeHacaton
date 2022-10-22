using Newtonsoft.Json;

namespace HendInRentApi.Dto.Inventory
{

   

    public class OutputHERAPointDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }
        [JsonProperty("place_text")]
        public string? PlaceText { get; set; }
        [JsonProperty("place_id")]
        public int PlaceId { get; set; }
    }


}
