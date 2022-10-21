using FluentAssertions;
using HendInRentApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Web.Dtos.UserSelfInfoDto.Rent;
using Web;
using Web.Controllers;
using Web.Geolocation;
using Web.Models;
using Web.Services;
using static Tests.Helper;
using Web.PasswordHasher;
using Web.Search.Inventory;

namespace Tests
{
    public class WebTests
    {
        WebApplicationFactory<Program> _factory;
        IServiceProvider _serviceProvider;
        AuthRentInHendApi _authRentInHend;





        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Program>();
            _serviceProvider = _factory.Services.CreateScope().ServiceProvider;
            _authRentInHend = _serviceProvider.GetRequiredService<AuthRentInHendApi>(); 
        }

        [Test]
        public async Task TestUserRegistration()
        {
            var user = GetRegistreUserFromJsonFile<UserRegistrationModel>();

            var controller = _serviceProvider.GetRequiredService<UserController>();

            var res = await controller.RegistrateUser(user);

            var jsonResult = res.As<JsonResult>();

            Assert.Pass("json is\n{0}", Serialize(jsonResult.Value));
        }

        [Test]
        public async Task TestUserLogin()
        {
            var controller = _serviceProvider.GetRequiredService<UserController>();

            var user = GetLoginUserFromJsonFile<UserLoginModel>();

            var res = await controller.LoginUser(user);

            var jsonRes = res.As<JsonResult>();

            Assert.Pass(Serialize(jsonRes.Value));
        }

       
        [Test]
        public async Task TestGeolocationApi()
        {
            var api = _serviceProvider.GetRequiredService<GeolocationRepository>();

            var city = await api.GetUserLocationByLatLon(55.878, 37.653);
            Assert.Pass(city.City);
        }

        [Test]
        public void TestJsonUser()
        {
            var userLogin = GetLoginUserFromJsonFile<UserLoginModel>();
            var userReg = GetRegistreUserFromJsonFile<UserRegistrationModel>();

            Assert.Pass("login:\n{0}\nregistration:\n{1}\n",Serialize(userLogin), Serialize(userReg));
        }
        [Test]
        public async Task TestProfileService()
        {
            var profileServise = _serviceProvider.GetRequiredService<SelfInfoService>();
            var token = await GetRentInHendTokenForTesting(_authRentInHend);

            var res = await profileServise.GetUserProfileSelfInfo(token);

            var strRes = Serialize(res);

            Assert.Pass("profile:\n{0}", strRes);
        }

        [Test]
        public async Task RentSelfInfoServiceTest()
        {
            var serv = _serviceProvider.GetRequiredService<SelfInfoService>();
            var token = await GetRentInHendTokenForTesting(_authRentInHend);
            var res = await serv.GetUserRentSelfInfo(token);

            Assert.Pass("rent:\n{0}", Serialize(res));
        }
        [Test]
        public async Task RentSelfInfoServiceTestViewJustOneObject()
        {
            var serv = _serviceProvider.GetRequiredService<SelfInfoService>();
            var token = await GetRentInHendTokenForTesting(_authRentInHend);
            var resService = await serv.GetUserRentSelfInfo(token);
            var res = resService.Array.First();

            Assert.Pass("rent:\n{0}", Serialize(res));
        }


        [Test]
        public async Task SaleServiceTest()
        {
            var serv = _serviceProvider.GetRequiredService<SaleService>();
            var token = await GetRentInHendTokenForTesting(_authRentInHend);
            var res = await serv.GetInventory(token);
            Assert.Pass("rent:\n{0}", Serialize(res));
        }

        [Test]
        public async Task SaleServiceGetInventory()
        {
            var serv = _serviceProvider.GetRequiredService<SaleService>();

            var envent = await serv.GetInventories(new Web.Dtos.Sales.Inventory.InputSearchInventoryDto { Tags = null, Search=null });

            foreach (var env in envent)
            {
                Assert.Pass(Serialize(env));
            }
        }
        [Test]
        public void HasherTest()
        {
            var password = "EQWEQWE";
            var hasher = _serviceProvider.GetRequiredService<IPasswordHasher>();
            var hash = hasher.Hash(password);

            hash.Should().NotBeNull();
            Assert.Pass("password:\n{0}\nhash:\n{1}", password, hash);
        }

        [Test]
        public void TagSearcher()
        {
            var searcher = _serviceProvider.GetRequiredService<InventoryTagSearcher>();

            var isContained= searcher.TagsIsContained(new string[] {"лыжи"}, "Лыжи на прокат");

            isContained.Should().BeTrue();
        }

        [Test]
        public async Task SaleControllerTest()
        {
            var contr = _serviceProvider.GetRequiredService<SalesController>();

            var res = (await contr.GetInventory(new Web.Models.Inventory.InventorySearchModel { Search = null, Tags = new string[] {"лыжи" } })).As<JsonResult>();
            res.Should().NotBeNull();
            Assert.Pass(Serialize(res.Value));
        }
    }
}
