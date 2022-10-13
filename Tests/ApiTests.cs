using System.Net;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using FluentAssertions;
using System;
using HendInRentApi;
using static Tests.Helper;
using static HendInRentApi.AllConstants;
using Microsoft.Extensions.Options;

namespace Tests
{

    public class ApiTests
    {

        AuthApi AuthApi => new AuthApi();
        UniversalApi UniversalApi => new UniversalApi();
        InventoryApi InventoryApi => new InventoryApi();

        InputLoginUserDto UserToLogin => new InputLoginUserDto { Login = "", Password = "" };


        [SetUp]
        public void Setup()
        {
            

            
        }

       

        [Test]
        public async Task AuthTest()
        {
            var res = await AuthApi.Login(UserToLogin);

            res.Should().NotBeNull().And.Match<OutputAuthTokenDto>(u => u.AccessToken != null);
            var str = Serialize(res);
            Assert.Pass("user:\n{0}", str);
        }

        [Test]
        public async Task InventoryTest()
        {
            var authToken = await AuthApi.Login(UserToLogin);

            var inputDto = new InputInventoryDto { };


            var invent = await InventoryApi.PostInvetoryItems(authToken.AccessToken, inputDto);

            var str = Serialize(invent);
            Assert.Pass("response: {0}", str);
        }


        [Test]
        public async Task UniverseApiTest()
        {
            var authToken = await AuthApi.Login(UserToLogin);

            var invent = await UniversalApi.MakePostJsonTypeRequest<OutputInventoriesDto, InputInventoryDto>
                (POST_INVENTORY_ITEMS, authToken.AccessToken, new InputInventoryDto {Search = "очки" });

            var str = Serialize(invent);

            Assert.Pass("response: {0}", str);
        }

        [Test]
        public async Task NonCorrectDataTest()
        {
            var authToken = await AuthApi.Login(UserToLogin);

            var invent = await UniversalApi.MakePostJsonTypeRequest<OutputAuthTokenDto, InputInventoryDto>
                (POST_INVENTORY_ITEMS, authToken.AccessToken, new InputInventoryDto { Search = "очки" });

            var str = Serialize(invent);

            Assert.Pass("response: {0}", str);
        }
    }
}