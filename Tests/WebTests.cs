using FluentAssertions;
using FluentValidation;
using HendInRentApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Web;
using Web.Controllers;
using Web.HasingToken;
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
        public void TestAuthInRentInHendApiService()
        {
            var t =_factory.Services.GetService(typeof(AuthRentInHendApi)) as AuthRentInHendApi;

            t.Should().NotBeNull();
        }

        [Test]
        public void TestControllerIsNotNull()
        {
            var controller = _serviceProvider.GetRequiredService<IValidator<UserRegistrationModel>>();

            

            controller.Should().NotBeNull();
        }
        [Test]
        public async Task TestUserRegistration()
        {
            var controller = _serviceProvider.GetRequiredService<UserController>();

            var res = await controller.RegistrateUser(new UserRegistrationModel {Login = "vitalcik.kovalenko2019@gmail.com", Password = null, City = "taganrog", Email = "vitalcik.kovalenko2019@gmail.com", Telephone = "vitalcik.kovalenko2019@gmail.com" });

            var jsonResult = res.As<JsonResult>();

            Assert.Pass("json is\n{0}", Serialize(jsonResult.Value));
        }

        [Test]
        public async Task TestCryptography()
        {
            var service = _serviceProvider.GetRequiredService<TokenCryptographer>();


            var str = "STR";
            var encr = service.Encrypt(str);
            var decr = service.Decrypt(encr);

            str.Should().Match(s => s == decr);
        }
        
    }
}