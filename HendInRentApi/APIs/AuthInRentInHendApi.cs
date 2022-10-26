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
            // используется метод отправки запросов от майков т.к. только с ним авторизация работает
            // поэтому если использовать атрибуты json для привязки к полям в dto,
            // чтобы отправить логин и пароль, нужно использоватьиз библиотеку System.Net.Http.Json

            await response.StatusIsOKOrThrowException(path);

            var result = await response.Content.ReadJsonByNewtonsoft<OutputHIRAAuthTokenDto>() ?? throw new NullReferenceException();
            //а вот для объектов, которые присылаеются с сервера нужно использовать библиотеку уже от Newtonsoft.json
            
            // почему так:
            // как было сказано ранее сериалязер от майков при запросах работает не так как надо, 
            // поэтому в основном я использую свои методы отправки запроса, где используется json сериалязер от newtonsoft
            
            // но этот метод исключение из правил т.к. авторизация с newtonsoft здесь почему-то не работает. я думаю это проблема api
            
            return result;
        }
    }
}
