
using System.Net;
using System.Net.Http.Json;

using System.Net.Http.Headers;
using FluentAssertions;
using static Tests.HttpClientFactoryAndConstants;
using static Tests.AllConstants;

namespace Tests
{

    #region helpers

    public static class AllConstants
    {
        public const string APITOKEN = ""; //����� �� �������, ����� �������� �� AuthApi.Login ������

        public const string APIURL = @"https://api.rentinhand.ru"; // ������ �� ������� ����, ����� �� ��������� �
    }



    public static class HttpClientFactoryAndConstants
    {
       

        public static void AddHeaders(this HttpClient client, string bearer_token) 
        // ����� �����, ����� ������ ��� �� ������ �������� ��� �������
        // � ����� ������(json, string � ��.)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer_token);// ��� �����. �.�. �� ����� ����� ���� ������,
            // �� ��� ����� ����� ��� ��������
            // � ����� 100500 ������� �� �������
            client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", "pnsXw4CP4HIdF2eoWuPPCStqmPdKhWHLlJzoQMFJ");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        }
        public static HttpClient GetClientWithHeaders(string bearer_token) // �������� ��� �������� ������ ���� �
        {
            HttpClient client = new HttpClient();
            client.AddHeaders(bearer_token);
            return client;
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
    public class InvetoryRequest
    {
        public string search { get; set; } = null!;
        public string title { get; set; } = null!;
        public string description { get; set; } = null!;
        public string rent_number { get; set; } = null!;
        public int state_id { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public DiscountsRequest discounts { get; set; } = null!;
    }


    public class DiscountRequest
    {
        public int id { get; set; }
        public int title { get; set; }
        public int price { get; set; }
    }

    public class DiscountsRequest
    {
        public DiscountRequest discount { get; set; } = null!;
        public int discount_id { get; set; }
        public int resource_id { get; set; }
    }




    public class InventoryItemResponse
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
        public StateResponse state { get; set; }
        public PointResponse point { get; set; }
        public PriceResponse price { get; set; }
        public DiscountsResponse discounts { get; set; }
    }



    public class DiscountResponse
    {
        public int id { get; set; }
        public int title { get; set; }
        public int price { get; set; }
    }

    public class DiscountsResponse
    {
        public DiscountResponse discount { get; set; }
        public int discount_id { get; set; }
        public int resource_id { get; set; }
    }

    public class PermissionResponse
    {
        public int resource_id { get; set; }
        public bool delete { get; set; }
        public bool read { get; set; }
        public bool write { get; set; }
        public bool right { get; set; }
    }

    public class PlaceResponse
    {
        public int osm_id { get; set; }
        public string display_name { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string lon { get; set; }
        public string lat { get; set; }
    }

    public class PointResponse
    {
        public int id { get; set; }
        public string title { get; set; }
        public string article { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string phone { get; set; }
        public string place_text { get; set; }
        public int place_id { get; set; }
        public PlaceResponse place { get; set; }
    }

    public class PriceResponse
    {
        public string title { get; set; }
        public string price_logic_id { get; set; }
        public int point_id { get; set; }
    }

    public class InventoryItemsPostResult
    {
        public List<InventoryItemResponse> array { get; set; }
        public string message { get; set; }
        public PermissionResponse permission { get; set; }
    }

    public class StateResponse
    {
        public int id { get; set; }
        public string title { get; set; }
        public string @const { get; set; }
        public string text_color { get; set; }
        public string color { get; set; }
    }

    #endregion
    #endregion

    #region apies
    public class AuthApi
    {
        static string auth_uri = APIURL + "/v1/login"; // �������� � ���� ���������
        public async Task<AuthToken> Login(string login, string password) //������ ������ �� ���� post /v1/login � json �������� � json �������
        {
            HttpClient client = GetClientWithHeaders(APITOKEN);

            var login_data = new {login = login, password = password }; // ������ ��������, ����� ������� ������.
            // ������������ ��������� �����, ����� �� ������� ��� ����� � ������

            
            var response = await client.PostAsJsonAsync(auth_uri, login_data); // ������ ������
            if (!response.IsSuccessStatusCode) // ��������� ��� ��� ������ ���� ����� �� ��
            {
                var mess = new { error = "" }; // ���� ���������� ���������
                var jsonContent = await response.Content.ReadFromJsonAsync(mess.GetType());
                throw new Exception($"Status code is {(int)response.StatusCode}" +
                    $"({response.StatusCode})\nHeaders:\n{response?.RequestMessage?.Headers.ToString()}\nQuery:\n{auth_uri}\nmessage:\n{jsonContent}"); //���������� ������
                //�� �������������� �� �������� �������� - ��� ����
            }
            var res = await response.Content.ReadFromJsonAsync<AuthToken>() ?? throw new NullReferenceException(); // ����������� ����, ����� ���������� �� ������� �� null ���.
            

            return res;
            
        }

        


        public void Logout()
        {

        }
    }

    public class InventoryApi
    {
        static readonly string get_invetory_url = APIURL + "/v1/inventory/items"; 

        //public InvetoryRequest GetInvetory()
        //{
        //    var client = GetClientWithHeaders(APITOKEN);

        //    client.
        //}
    }
    #endregion

    public class MainTests
    {
        [SetUp]
        public void Setup()
        {
        }

        AuthApi GetAuthApi() => new AuthApi();

        [Test]
        public async Task Auth_Test()
        {
            var api = GetAuthApi();

            var auth = await api.Login(login:"", password:"");
            auth.Should().NotBeNull().And.Match<AuthToken>(d => d.token_type != null && d.access_token != null);
            Assert.Pass("token is {0}", auth.access_token);
        }
    }
}