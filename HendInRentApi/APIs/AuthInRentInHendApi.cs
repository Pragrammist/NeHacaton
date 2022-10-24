using System.Net.Http.Json;
using static HendInRentApi.RentInHendApiConstants;
using static HendInRentApi.HttpStaticMethod;

namespace HendInRentApi
{
    public class AuthRentInHendApi : BaseMethodsApi, HIRALogin<OutputHIRAAuthTokenDto, InputHIRALoginUserDto>
    {
        private static string auth_uri = API_URL + POST_AUTH_LOGIN; 
        
        public async Task<OutputHIRAAuthTokenDto> Login(InputHIRALoginUserDto user) 
        {
            HttpClient client = GetClientWithoutBearer();
            
            return await MakeJsonTypeRequest<OutputHIRAAuthTokenDto, InputHIRALoginUserDto>(auth_uri, user, client.PostAsJsonAsyncByNewtonsoft);
        }
    }
}
