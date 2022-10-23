using System.Net.Http.Json;
using static HendInRentApi.RentInHendApiConstants;

namespace HendInRentApi
{
    public class AuthRentInHendApi : HIRALogin<OutputHIRAAuthTokenDto, InputHIRALoginUserDto>
    {
        private static string auth_uri = API_URL + POST_AUTH_LOGIN; 
        
        public async Task<OutputHIRAAuthTokenDto> Login(InputHIRALoginUserDto user) 
        {
            HttpClient client = new HttpClient();
            client.AddHeadersWithoutBearer(); 

            var response = await client.PostAsJsonAsync(auth_uri, user); 

            await response.StatusIsOKOrThrowException(auth_uri); 

            var result = await response.Content.ReadJsonByNewtonsoft<OutputHIRAAuthTokenDto>() ?? throw new NullReferenceException();
        
            return result;
        }
    }
}
