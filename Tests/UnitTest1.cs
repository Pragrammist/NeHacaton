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
        public const string APITOKEN = ""; //token ot akaunta ego mozno vzat iz AuthApi.Login

        public const string APIURL = @"https://api.rentinhand.ru"; // uri for api
    }



    public static class HttpStaticMethod
    {
       
        //method nuzhen dlya dobavleniya tokena v zaprosi. takzhe nekotorih drugih nastroek
        public static void AddHeaders(this HttpClient client, string bearer_token) 
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer_token);// dobavlenia tokena
           
            client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", "pnsXw4CP4HIdF2eoWuPPCStqmPdKhWHLlJzoQMFJ"); // eto ya prosto udivel v api, poetomu prosto dobavil
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //for json
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json"); //for json hotya eto ne rabotoent no est
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
                var jsonStringResponse = await response.Content.ReadAsStringAsync(); // chitaem ovet esli est


                //zdec snachalo obvorachivaentsa v json a potom iz jsona polachaetsa stroka t.k. inache ne pravilno otobrazaet symbols
                //tam budet unicode symbols no ne symbols
                var jobj = JObject.Parse(jsonStringResponse); // poluchaem json kak object
                var stringMessage = jobj.ToString();//prevrachaem v stroku

                throw new Exception($"Status code is {(int)response.StatusCode}" + //perenos dly krasoti
                    $"({response.StatusCode})\nQuery: {authUri}\nmessage:\n{stringMessage}"); //vivod oshibki
                
            }
        }

        public static async Task<JObject> ReadAsJObject(this HttpContent content) //nuzjen chotbi soderjimoe prevrachat d json object dly otobrojenia d testi
        //eto vremeniy method, navenroye
        {
            var readStirng = await content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(readStirng);
            return jsonObject;

        }
    }


    

    public static class JsonStaticMethod
    {
        public static JsonSerializerOptions GetGlobalJsonSerializerOptions() // nastroyka serelizacii - tut nechego commentirovat
        {
            var options = new JsonSerializerOptions();
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            //options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            return options;
        }
    }
    #endregion
    //objects. commentirovat ne budu. commentarii est v documentation k api
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

    //eto obolochki nad api chtobi kazdiy raz ne pisat http zaprosi
    public class AuthApi
    {
        static string auth_uri = APIURL + "/v1/login"; //kuda delat zapros
        public async Task<AuthToken> Login(string login, string password) //delaet zapros po puti /v1/login
        {
            HttpClient client = GetClientWithHeaders(APITOKEN); // ta samaya nastroyka tokena, no zdec eto ne nuzhno eto prosto est. Zdec nuzna nastroyka X-CSRF-TOKEN

            var login_data = new {login = login, password = password }; // accaunt
            

            
            var response = await client.PostAsJsonAsync(auth_uri, login_data); // zapros - zdec ya ispolzuyu kak raz api

            await response.StatusIsOKOrThrowException(auth_uri); // esli ne OK to vidaet oshibku


            var jsonOption = GetGlobalJsonSerializerOptions(); //dlya nastroyki serelizacii - prosto nado


            var result = await response.Content.ReadFromJsonAsync<AuthToken>(jsonOption) ?? throw new NullReferenceException(); 
            //zdec mi poluchaem c# object iz otveta

            return result;
            
        }

        


        public void Logout()
        {
            //potom sdelayu
        }
    }

    public class InventoryApi
    {
        static readonly string get_invetory_url = APIURL + "/v1/inventory/items"; // puty dlya poluchenia inventory
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestData">soderzhit chto iskat</param>
        /// <param name="token">token</param>
        /// <returns></returns>
        public async Task<JObject> PostInvetoryItems(RequestInventory? requestData = null, string token = APITOKEN)
        {
            var client = GetClientWithHeaders(token); // ne budu povtoryat
            
            var response = await client.PostAsJsonAsync(get_invetory_url, requestData); // zapros

            await response.StatusIsOKOrThrowException(get_invetory_url); //uze bilo
            

            //var result =  await response.Content.ReadFromJsonAsync<ResponseInventoryItemsResult>(jsonOptions) ?? throw new NullReferenceException("result of HttpContent.ReadFromJsonAsync is null");
            var jobject = await response.Content.ReadAsJObject();
            return jobject;


            // voobshe vmesto JObject dolzen bit ResponseInventoryItemsResult, no poka s etim problemi
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

            var auth = await api.Login(login:"", password:"");
            auth.Should().NotBeNull().And.Match<AuthToken>(d => d.token_type != null && d.access_token != null); // eto proverka na pravilnost otveta
            Assert.Pass("token is {0}", auth.access_token);
        }

        [Test]
        public async Task InventoryTest()
        {
            var api = GetInventoryApi();

            var inventoriesRes = await api.PostInvetoryItems(); // otvet
            
            

            Assert.Pass("{0}", inventoriesRes.ToString());
        }
    }
}