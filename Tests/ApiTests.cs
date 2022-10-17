using FluentAssertions;
using HendInRentApi;
using static Tests.Helper;
using static HendInRentApi.RentInHendApiConstants;


namespace Tests
{
    public class ApiTests
    {
        AuthRentInHendApi AuthApi => new AuthRentInHendApi();
        GenericRepositoryApi UniversalApi => new GenericRepositoryApi();
        InventoryRepositoryApi InventoryApi => new InventoryRepositoryApi();
        InputLoginUserRentInHendDto UserToLogin => new InputLoginUserRentInHendDto { Login = "0", Password = "0" };

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
