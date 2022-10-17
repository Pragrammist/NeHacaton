using FluentAssertions;
using FluentValidation;
using HendInRentApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Net.Http.Json;
using System.Reflection;
using Web;
using Web.Controllers;
using Web.Geolocation;
using Web.Models;
using static Tests.Helper;

namespace Tests
{
    public class WebTests
    {
        WebApplicationFactory<Program> _factory;
        IServiceProvider _serviceProvider;

        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Program>();
            _serviceProvider = _factory.Services.CreateScope().ServiceProvider;
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
    }
}
