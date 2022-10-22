using FluentAssertions;
using HendInRentApi;
using static Tests.Helper;
using static HendInRentApi.RentInHendApiConstants;
using HendInRentApi.Dto.SelfInfo.Profile;
using HendInRentApi.Dto.SelfInfo.Rent;
using HendInRentApi.Dto.Inventory;

namespace Tests
{
    public class ApiTests
    {
        AuthRentInHendApi AuthApi => new AuthRentInHendApi();
        GenericRepositoryApi UniversalApi => new GenericRepositoryApi();
        InputHERALoginUserRentInHendDto UserToLogin => GetLoginUserFromJsonFile<InputHERALoginUserRentInHendDto>();


        [SetUp]
        public void Setup()
        {            
        }       

        [Test]
        public async Task AuthTest()
        {
            var res = await AuthApi.Login(UserToLogin);

            res.Should().NotBeNull().And.Match<OutputHEARAuthTokenDto>(u => u.AccessToken != null);
            var str = Serialize(res);
            Assert.Pass("user:\n{0}", str);
        }

        [Test]
        public async Task InventoryTest()
        {
            var authToken = await AuthApi.Login(UserToLogin);

            var inputDto = new InputHERAInventoryDto {Search = "лыжи" };


            var invent = await UniversalApi.MakePostJsonTypeRequest<OutputHERAInventoriesResultDto, InputHERAInventoryDto>(POST_INVENTORY_ITEMS, authToken.AccessToken, inputDto);

            //var invent = await InventoryApi.PostInvetoryItems(authToken.AccessToken, inputDto);

            var str = Serialize(invent);
            Assert.Pass("response: {0}", str);
        }

        [Test]
        public async Task UniverseApiTest()
        {
            var authToken = await AuthApi.Login(UserToLogin);

            var invent = await UniversalApi.MakePostJsonTypeRequest<OutputHERAInventoriesResultDto, InputHERAInventoryDto>
                (POST_INVENTORY_ITEMS, authToken.AccessToken, new InputHERAInventoryDto {Search = "очки" });

            var str = Serialize(invent);

            Assert.Pass("response: {0}", str);
        }

        [Test]
        public async Task NonCorrectDataTest()
        {
            var authToken = await AuthApi.Login(UserToLogin);

            var invent = await UniversalApi.MakePostJsonTypeRequest<OutputHEARAuthTokenDto, InputHERAInventoryDto>
                (POST_INVENTORY_ITEMS, authToken.AccessToken, new InputHERAInventoryDto { Search = "очки" });

            var str = Serialize(invent);

            Assert.Pass("response: {0}", str);
        }
        [Test]
        public async Task ProfileDataApiTest()
        {
            var authToken = await AuthApi.Login(UserToLogin);

            var profile = await UniversalApi.MakePostJsonTypeRequest<OutputHERAProfileSelfInfoResultDto>(POST_PROFILE, authToken.AccessToken);

            var str = Serialize(profile);

            Assert.Pass("response:\n{0}", str);
        }
        [Test]
        public async Task RentDataTest()
        {
            var authToken = await AuthApi.Login(UserToLogin);
            var input = new InputHERARentSearchDto();

            var data = await UniversalApi.MakePostJsonTypeRequest<OutputHERARentsResultDto>(POST_RENT, authToken.AccessToken);

            Assert.Pass("res:\n{0}",Serialize(data));
        }
    }
}
