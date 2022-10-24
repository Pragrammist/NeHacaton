using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static System.Net.Mime.MediaTypeNames;

namespace HendInRentApi
{
    public static class HttpStaticMethod
    {
        public static void AddHeadersWithoutBearer(this HttpClient client)
        {
            client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", "pnsXw4CP4HIdF2eoWuPPCStqmPdKhWHLlJzoQMFJ");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        }

        public static void AddBearer(this HttpClient client, string bearer_token)
        {
            client.AddHeadersWithoutBearer();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer_token);
        }

        public static HttpClient GetClientWithoutBearer()
        {
            HttpClient client = new HttpClient();
            client.AddHeadersWithoutBearer();
            return client;
        }

        public static HttpClient GetClientWithBearer(string bearer_token)
        {
            HttpClient client = new HttpClient();
            client.AddBearer(bearer_token);
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



            var serealizer = JsonSerializer.Create(NullValueHandlingIgnoreSetting);

            try
            {
                var obj = serealizer.Deserialize<T>(reader);
                return obj;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"with content:\n{stringContent}\n", ex);
            }
            finally
            {
                textReader.Dispose();
            }
        }

        static async Task<HttpResponseMessage> RequestAsJsonAsync<TArg>(string path, TArg arg, Func<string, HttpContent, CancellationToken, Task<HttpResponseMessage>> AsyncMethod,
            JsonSerializerSettings? settings = null, CancellationToken cancellationToken = default)
        {
            try
            {
                settings = settings ?? NullValueHandlingIgnoreSetting;
                var serializer = JsonSerializer.Create(settings);
                var json = JObject.FromObject(arg, serializer).ToString();
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                return await AsyncMethod(path, content, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"with arg:\n", ex);
            }
            finally
            {

            }
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsyncNewtonsoft<TArg>(this HttpClient client, string path, TArg arg, JsonSerializerSettings? settings = null,
            CancellationToken cancellationToken = default) => await RequestAsJsonAsync(path, arg, client.PostAsync, settings, cancellationToken);

        public static async Task<HttpResponseMessage> PutAsJsonAsyncNewtonsoft<TArg>(this HttpClient client, string path, TArg arg, JsonSerializerSettings? settings = null,
            CancellationToken cancellationToken = default) => await RequestAsJsonAsync(path, arg, client.PutAsync, settings, cancellationToken);

        private static JsonSerializerSettings NullValueHandlingIgnoreSetting => new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
    }
}
