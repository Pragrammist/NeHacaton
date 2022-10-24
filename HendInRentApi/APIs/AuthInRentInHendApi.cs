using System.Net.Http.Json;
using static HendInRentApi.RentInHendApiConstants;
using static HendInRentApi.HttpStaticMethod;

namespace HendInRentApi
{
    public class AuthRentInHendApi : BaseMethodsApi, HIRALogin<OutputHIRAAuthTokenDto, InputHIRALoginUserDto>
    {
        static string path = API_URL + POST_AUTH_LOGIN;
        public async Task<OutputHIRAAuthTokenDto> Login(InputHIRALoginUserDto user) 
        {
            HttpClient client = GetClientWithoutBearer();
            

            var response = await client.PostAsJsonAsync(path, user);

            await response.StatusIsOKOrThrowException(path);

            var result = await response.Content.ReadJsonByNewtonsoft<OutputHIRAAuthTokenDto>() ?? throw new NullReferenceException();

            return result;
        }
    }
}
