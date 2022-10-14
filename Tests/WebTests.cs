using FluentAssertions;
using HendInRentApi;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Web;
using static Tests.Helper;

namespace Tests
{

    

    public class WebTests
    {
        WebApplicationFactory<Program> _factory;


        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Program>();
        }

        [Test]
        public async Task TestRequest()
        {
            var client = _factory.CreateClient();

            var t = await client.GetFromJsonAsync<OutputAuthTokenDto>("/Home/Index/");

            client.Dispose();

            Assert.Pass(Serialize(t));
        }
        [Test]
        public async Task TestService()
        {
            var t =_factory.Services.GetService(typeof(AuthInRentInHendApi)) as AuthInRentInHendApi;

            t.Should().NotBeNull();
        }
    }
}