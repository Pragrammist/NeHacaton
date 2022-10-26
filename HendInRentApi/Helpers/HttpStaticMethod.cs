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
            // метод глобально используется когда делается запрос для логина в апи, и как вспомогательный ниже
        {
            client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", "pnsXw4CP4HIdF2eoWuPPCStqmPdKhWHLlJzoQMFJ"); 
            //для этого токена придется отдельно писать конфигурацию в этом проекте, 
            //его нельзя засунуть в appsettings.json т.к. это породит завимость для текущего проекта
            
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        }

        public static void AddBearer(this HttpClient client, string bearer_token)
        {
            client.AddHeadersWithoutBearer();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer_token);
        }

        public static HttpClient GetClientWithoutBearer()// метод-адаптер
        {
            HttpClient client = new HttpClient();
            client.AddHeadersWithoutBearer();
            return client;
        }

        public static HttpClient GetClientWithBearer(string bearer_token) //метод-адаптер
        {
            HttpClient client = new HttpClient();
            client.AddBearer(bearer_token);
            return client;
        }



        public static async Task StatusIsOKOrThrowException(this HttpResponseMessage response, string authUri) 
            // у майков конечно есть своя реализа этого метода, но мне нужна настройка вывода и исключениея
        {
            if (!response.IsSuccessStatusCode)
            {
                var messageJobj = await response.Content.ReadAsJObject(); //просто как строку не прочитать как выводятся не символы а их юникод
                throw new HttpRequestException($"Status code is {(int)response.StatusCode}" +
                $"({response.StatusCode})\nQuery: {authUri}\nmessage:\n{messageJobj}");

            }
        }

        public static async Task<JObject> ReadAsJObject(this HttpContent content) 
            // в основном используется когда нужно использовать json без создания класса, как выше например
        {
            var readStirng = await content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(readStirng);
            return jsonObject;
        }

        public static async Task<T> ReadJsonByNewtonsoft<T>(this HttpContent content) 
            // здесь используется снова своя реализация похожего метода майков т.к. у них свой сериалязер, который поломанный
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

        static async Task<HttpResponseMessage> RequestAsJsonAsync<TArg>(string path, TArg arg, 
            Func<string, HttpContent, CancellationToken, Task<HttpResponseMessage>> AsyncMethod, // чтобы меньше писать кода сюда перадается метод
            // котоырй будет делать сам запрос, а ниже просто подготовка данных для запроса
            JsonSerializerSettings? settings = null, CancellationToken cancellationToken = default)
        {
            try
            {
                settings = settings ?? NullValueHandlingIgnoreSetting;
                var serializer = JsonSerializer.Create(settings);
                var json = JObject.FromObject(arg, serializer).ToString();
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                return await AsyncMethod(path, content, cancellationToken); // тот самый запрос про который выше говорилось
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"with arg:\n", ex);
            }
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsyncNewtonsoft<TArg>(this HttpClient client, string path, TArg arg, JsonSerializerSettings? settings = null,
            CancellationToken cancellationToken = default) => await RequestAsJsonAsync(path, arg, client.PostAsync /*операция запроса*/, settings, cancellationToken);
         // снова-метод адаптер, который просто использует метод для формирования данных для запроса 
         // и метод отправки запроса(POST), который передается в аргументы
        public static async Task<HttpResponseMessage> PutAsJsonAsyncNewtonsoft<TArg>(this HttpClient client, string path, TArg arg, JsonSerializerSettings? settings = null,
            CancellationToken cancellationToken = default) => await RequestAsJsonAsync(path, arg, client.PutAsync/*операция запроса*/, settings, cancellationToken);
        // все как выше только для put 
        private static JsonSerializerSettings NullValueHandlingIgnoreSetting => new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
        // эта настройка нужна чтобы при запросы апи nullable объекты незаписывались и апи не пытался по null искать что-то
        // также если с сервера прилитает null, там где может храниться число, чтобы это игнорировалось и язык сам дописывал дефолтное значение числа
    }
}
