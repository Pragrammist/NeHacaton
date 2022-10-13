using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using FluentAssertions;
using static Tests.HttpStaticMethod;
using static Tests.AllConstants;
using static Tests.JsonStaticMethod;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tests
{

    #region helpers

    public static class AllConstants
    {
        public const string APITOKEN = ""; //токен от аккунта, можно получить из AuthApi.Login метода

        public const string APIURL = @"https://api.rentinhand.ru"; // ссылка на главный сайт, чтобы ее соединять в
    }



    public static class HttpStaticMethod
    {
       

        public static void AddHeaders(this HttpClient client, string bearer_token) 
        // метод нужен, чтобы каждый раз не писать рутинный код токеном
        // и типом ответа(json, string и пр.)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer_token);// сам токен. п.с. не везде может быть нужуен,
            // но все равно будет для удобства
            // и чтобы 100500 методов не плодить
            client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", "pnsXw4CP4HIdF2eoWuPPCStqmPdKhWHLlJzoQMFJ");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        }
        public static HttpClient GetClientWithHeaders(string bearer_token) // обвертка для верхнего метода если п
        {
            HttpClient client = new HttpClient();
            client.AddHeaders(bearer_token);
            return client;
        }

        public static async Task StatusIsOKOrThrowException(this HttpResponseMessage response, string authUri)// метод нужен, т.к. происходит работа с стороним api 
            //и нужно знать почему ответ не пришел
        {
            if (!response.IsSuccessStatusCode) 
            {
                var jsonStringResponse = await response.Content.ReadAsStringAsync();
                var jobj = JObject.Parse(jsonStringResponse);
                var stringMessage = jobj.ToString();
                throw new Exception($"Status code is {(int)response.StatusCode}" +
                    $"({response.StatusCode})\nQuery: {authUri}\nmessage:\n{stringMessage}"); 
                //показываем ошибку на замудеренность не обращать внимание - так надо
            }
        }

        public static async Task<JObject> ReadAsJObject(this HttpContent content)
        {
            var readStirng = await content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(readStirng);
            return jsonObject;

        }
    }


    

    public static class JsonStaticMethod
    {
        public static JsonSerializerOptions GetGlobalJsonSerializerOptions()
        {
            var options = new JsonSerializerOptions();
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            //options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            return options;
        }
    }
    #endregion

    #region json objs

    #region user objs
    public class AuthToken
    {
        public string access_token { get; set; } = null!;
        public int expires_in { get; set; } 
        public string token_type { get; set; } = null!;
    }
    #endregion /

    #region inventory objs

    public class RequestDiscount
    {
        public int id { get; set; }
        public int title { get; set; }
        public int price { get; set; }
    }

    public class RequestDiscounts
    {
        public RequestDiscount discount { get; set; }
        public int discount_id { get; set; }
        public int resource_id { get; set; }
    }

    public class RequestInventory
    {
        public string search { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string rent_number { get; set; }
        public int state_id { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public RequestDiscounts discounts { get; set; }
    }
    public class ResponseInventoryArray
    {
        public int id { get; set; }
        public int parent_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string rent_number { get; set; }
        public int state_id { get; set; }
        public int buy_price { get; set; }
        public string buy_date { get; set; }
        public string option { get; set; }
        public ResponseState state { get; set; }
        public ResponsePoint point { get; set; }
        public ResponsePrice price { get; set; }
        public ResponseDiscounts discounts { get; set; }
    }

    public class ResponseDiscount
    {
        public int id { get; set; }
        public int title { get; set; }
        public int price { get; set; }
    }

    public class ResponseDiscounts
    {
        public ResponseDiscount discount { get; set; }
        public int discount_id { get; set; }
        public int resource_id { get; set; }
    }

    public class ResponsePermission
    {
        public int resource_id { get; set; }
        public bool delete { get; set; }
        public bool read { get; set; }
        public bool write { get; set; }
        public bool right { get; set; }
    }

    public class ResponsePlace
    {
        public int osm_id { get; set; }
        public string display_name { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string lon { get; set; }
        public string lat { get; set; }
    }

    public class ResponsePoint
    {
        public int id { get; set; }
        public string title { get; set; }
        public string article { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string phone { get; set; }
        public string place_text { get; set; }
        public int place_id { get; set; }
        public ResponsePlace place { get; set; }
    }

    public class ResponsePrice
    {
        public string title { get; set; }
        public string price_logic_id { get; set; }
        public int point_id { get; set; }
    }

    public class ResponseInventoryItemsResult
    {
        public List<ResponseInventoryArray> array { get; set; }
        public string message { get; set; }
        public ResponsePermission permission { get; set; }
    }

    public class ResponseState
    {
        public int id { get; set; }
        public string title { get; set; }
        public string @const { get; set; }
        public string text_color { get; set; }
        public string color { get; set; }
    }

    #endregion
    #endregion //

    #region apies
    public class AuthApi
    {
        static string auth_uri = APIURL + "/v1/login"; // например с этим соединять
        public async Task<AuthToken> Login(string login, string password) //делает запрос по пути post /v1/login с json запросом и json ответом
        {
            HttpClient client = GetClientWithHeaders(APITOKEN);

            var login_data = new {login = login, password = password }; // данные аккаунта, чтобы сделать запрос.
            // используется анонимный класс, чтобы не плодить еще класс в файлах

            
            var response = await client.PostAsJsonAsync(auth_uri, login_data); // делаем запрос

            await response.StatusIsOKOrThrowException(auth_uri);


            var jsonOption = new JsonSerializerOptions();
            jsonOption.NumberHandling = JsonNumberHandling.WriteAsString;


            var result = await response.Content.ReadFromJsonAsync<AuthToken>(jsonOption) ?? throw new NullReferenceException(); // выбрасывает искл, чтобы компилятор не ругался на null тип.
            

            return result;
            
        }

        


        public void Logout()
        {

        }
    }

    public class InventoryApi
    {
        static readonly string get_invetory_url = APIURL + "/v1/inventory/items";

        public async Task<JObject> PostInvetoryItems(RequestInventory requestData = null, string token = APITOKEN)
        {
            var client = GetClientWithHeaders(token);
            
            var response = await client.PostAsJsonAsync(get_invetory_url, requestData);

            await response.StatusIsOKOrThrowException(get_invetory_url);
            var jsonOptions = GetGlobalJsonSerializerOptions();

            //var result =  await response.Content.ReadFromJsonAsync<ResponseInventoryItemsResult>(jsonOptions) ?? throw new NullReferenceException("result of HttpContent.ReadFromJsonAsync is null");
            var jobject = await response.Content.ReadAsJObject();
            return jobject;
        }


        
    }
    #endregion

    public class MainTests
    {
        [SetUp]
        public void Setup()
        {
        }

        AuthApi GetAuthApi() => new AuthApi();
        InventoryApi GetInventoryApi() => new InventoryApi();

        [Test]
        public async Task AuthTest()
        {
            var api = GetAuthApi();

            var auth = await api.Login(login:"vitalcik.kovalenko2019@gmail.com", password:"1231414");
            auth.Should().NotBeNull().And.Match<AuthToken>(d => d.token_type != null && d.access_token != null);
            Assert.Pass("token is {0}", auth.access_token);
        }

        [Test]
        public async Task InventoryTest()
        {
            var api = GetInventoryApi();

            var inventoriesRes = await api.PostInvetoryItems();
            
            

            Assert.Pass("{0}", inventoriesRes.ToString());
        }
    }
}