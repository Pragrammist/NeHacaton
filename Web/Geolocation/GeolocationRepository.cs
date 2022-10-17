using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Web.Dtos;
using static Web.Constants.GeolocationConstants;

namespace Web.Geolocation
{
    public class GeolocationRepository
    {
        string ParseJsonObjectToFindCity(JObject res)
        {
            return res["suggestions"][0]["data"]["city"].ToString();
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
                    City = ParseJsonObjectToFindCity(res)
                };
            }
        }
    }
}

