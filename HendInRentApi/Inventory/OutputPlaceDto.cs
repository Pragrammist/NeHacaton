using Newtonsoft.Json;

namespace HendInRentApi
{


    public class OutputPlaceDto
    {
        [JsonProperty("osm_id")]
        public int OsmId { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public float Lon { get; set; }
        public float Lat { get; set; }
    }
  

}
