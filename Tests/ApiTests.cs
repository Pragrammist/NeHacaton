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
        BaseRepository BaseApi => new BaseRepository();
        InputHIRALoginUserDto UserToLogin => GetLoginUserFromJsonFile<InputHIRALoginUserDto>();


        [SetUp]
        public void Setup()
        {            
        }       

        [Test]
        public async Task Auth()
        {
            var res = await AuthApi.Login(UserToLogin);

            res.Should().NotBeNull().And.Match<OutputHIRAAuthTokenDto>(u => u.AccessToken != null);

            Assert.Pass("user:\n{0}", Serialize(res));
        }

        [Test]
        public async Task Inventory()
        {
            var authToken = await AuthApi.Login(UserToLogin);

            var inputDto = new InputHIRAInventoryDto {Search = "лыжи" };

            var invent = await BaseApi.MakePostJsonTypeRequest
                <OutputHIRAInventoriesResultDto, InputHIRAInventoryDto>(POST_INVENTORY_ITEMS, authToken.AccessToken, inputDto);

            Assert.Pass("response: {0}", Serialize(invent));
        }

        [Test]
        public async Task Universe()
        {
            var authToken = await AuthApi.Login(UserToLogin);

            var invent = await BaseApi.MakePostJsonTypeRequest<OutputHIRAInventoriesResultDto, InputHIRAInventoryDto>
                (POST_INVENTORY_ITEMS, authToken.AccessToken, new InputHIRAInventoryDto {Search = "очки" });

            Assert.Pass("response: {0}", Serialize(invent));
        }

        [Test]
        public async Task NonCorrectType()
        {
            var authToken = await AuthApi.Login(UserToLogin);

            var invent = await BaseApi.MakePostJsonTypeRequest<OutputHIRAAuthTokenDto, InputHIRAInventoryDto>
                (POST_INVENTORY_ITEMS, authToken.AccessToken, new InputHIRAInventoryDto { Search = "очки" });

            Assert.Pass("response: {0}", Serialize(invent));
        }
        [Test]
        public async Task ProfileData()
        {
            var authToken = await AuthApi.Login(UserToLogin);

            var profile = await BaseApi.MakePostJsonTypeRequest<OutputHIRAProfileSelfInfoResultDto, object>(POST_PROFILE, authToken.AccessToken, null);

            Assert.Pass("response:\n{0}", Serialize(profile));
        }
        [Test]
        public async Task RentData()
        {
            var authToken = await AuthApi.Login(UserToLogin);
            
            var data = await BaseApi.MakePostJsonTypeRequest<OutputHIRARentsResultDto, object>(POST_RENT, authToken.AccessToken, null);

            Assert.Pass("res:\n{0}",Serialize(data));
        }
    }
}
