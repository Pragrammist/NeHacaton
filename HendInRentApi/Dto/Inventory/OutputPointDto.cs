using Newtonsoft.Json;

namespace HendInRentApi
{

   

    public class OutputPointDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Article { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        [JsonProperty("place_text")]
        public string PlaceText { get; set; }
        [JsonProperty("place_id")]
        public int PlaceId { get; set; }
        public OutputPlaceDto Place { get; set; }
    }


}
