using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HendInRentApi
{
    public static class HttpStaticMethod
    {

        //method nuzhen dlya dobavleniya tokena v zaprosi. takzhe nekotorih drugih nastroek

        public static void AddHeadersWithoutBearer(this HttpClient client)
        {
            client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", "pnsXw4CP4HIdF2eoWuPPCStqmPdKhWHLlJzoQMFJ"); // eto ya prosto udivel v api, poetomu prosto dobavil
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //for json
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json"); //for json hotya eto ne rabotoent no est
        }

        public static void AddHeaders(this HttpClient client, string bearer_token)
        {
            client.AddHeadersWithoutBearer();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer_token);// dobavlenia tokena
        }


        public static HttpClient GetClientWithHeaders(string bearer_token) // eto prosto obvertka dlya verhnego method
        {
            HttpClient client = new HttpClient();
            client.AddHeaders(bearer_token);
            return client;
        }


        //dlya otobrazenia oshibok
        public static async Task StatusIsOKOrThrowException(this HttpResponseMessage response, string authUri)
        {
            if (!response.IsSuccessStatusCode)
            {
                var messageJobj = response.Content.ReadAsJObject(); 
                throw new Exception($"Status code is {(int)response.StatusCode}" + //perenos dly krasoti
                $"({response.StatusCode})\nQuery: {authUri}\nmessage:\n{messageJobj}"); //vivod oshibki

            }
        }

        public static async Task<JObject> ReadAsJObject(this HttpContent content) //nuzjen chotbi soderjimoe prevrachat v json object dlya
        //otobrojenia v testi or oshibok
        {
            var readStirng = await content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(readStirng);
            return jsonObject;

        }

        public static async Task<T> ReadJsonByNewtonsoft<T>(this HttpContent content)
        {
            var stringContent = await content.ReadAsStringAsync();
            var textReader = new StringReader(stringContent);
            var reader = new JsonTextReader(textReader);

            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.NullValueHandling = NullValueHandling.Ignore;


            var serealizer = Newtonsoft.Json.JsonSerializer.Create(jsonSettings);


            var obj = serealizer.Deserialize<T>(reader);

            textReader.Dispose();

            return obj;
        }
    }


}
