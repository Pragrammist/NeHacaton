using FluentAssertions;
using FluentValidation;
using HendInRentApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Web;
using Web.Controllers;
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
        public void TestUserValidation()
        {
            var controller = _serviceProvider.GetRequiredService<UserController>();

            var res = controller.RegistrateUser(new UserRegistrationModel { });

            var jsonRes = res.As<JsonResult>();

            Assert.Pass("json res:\n{0}",Serialize(jsonRes.Value));
        }
    }
}