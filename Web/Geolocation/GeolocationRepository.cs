using Newtonsoft.Json.Linq;
using Web.Dtos;
using static Web.Constants.GeolocationConstants;

namespace Web.Geolocation
{
    public class GeolocationRepository 
    {
        //todo interface
        string GetCityFromJson(JObject res)
        {
            return res["suggestions"][0]["data"]["city"].ToString().ToLower(); 
            // апи возращает много чего и чтобы не делать кучу объектов, чтобы получить город используется linq
            // поэтому это лучше не трогать, и это вынесено в отдельный метод
            // работает на честном слове
        }

        IHttpClientFactory _clientFactory;

        public GeolocationRepository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<OutputLocationDto> GetUserLocationByLatLon(double lat, double lon)
        {
            using (var _client = _clientFactory.CreateClient(GEOLOCATION_HTTPCLIENT_NAME))
            {
                var coordinates = new
                {
                    lat = lat,
                    lon = lon,
                };

                var response = await _client.PostAsJsonAsync(GEOLOCATION_API_URL, coordinates);
                var jsonString = await response.Content.ReadAsStringAsync();

                JObject res = JObject.Parse(jsonString);

                return new OutputLocationDto
                {
                    City = GetCityFromJson(res)
                };
            }
        }
    }
}

