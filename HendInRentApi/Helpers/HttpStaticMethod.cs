using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace HendInRentApi
{
    public static class HttpStaticMethod
    {
        public static void AddHeadersWithoutBearer(this HttpClient client)
        {
            client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", "pnsXw4CP4HIdF2eoWuPPCStqmPdKhWHLlJzoQMFJ"); 
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
        }

        public static void AddHeaders(this HttpClient client, string bearer_token)
        {
            client.AddHeadersWithoutBearer();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer_token);
        }


        public static HttpClient GetClientWithHeaders(string bearer_token)
        {
            HttpClient client = new HttpClient();
            client.AddHeaders(bearer_token);
            return client;
        }


        
        public static async Task StatusIsOKOrThrowException(this HttpResponseMessage response, string authUri)
        {
            if (!response.IsSuccessStatusCode)
            {
                var messageJobj = await response.Content.ReadAsJObject(); 
                throw new HttpRequestException($"Status code is {(int)response.StatusCode}" + 
                $"({response.StatusCode})\nQuery: {authUri}\nmessage:\n{messageJobj}"); 

            }
        }

        public static async Task<JObject> ReadAsJObject(this HttpContent content) 
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


            var serealizer = JsonSerializer.Create(jsonSettings);

            try
            {
                var obj = serealizer.Deserialize<T>(reader);
                return obj;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"with content:\n{stringContent}\n", ex);
            }
            finally
            {
                textReader.Dispose();
            }
        }

        
    }
}
