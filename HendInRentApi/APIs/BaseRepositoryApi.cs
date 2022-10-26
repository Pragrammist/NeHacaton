using static HendInRentApi.RentInHendApiConstants;
using static HendInRentApi.HttpStaticMethod;
using Newtonsoft.Json;
// далее читать GenericRepo
namespace HendInRentApi
{
    public class BaseMethodsApi // этот класс нужен чтобы инкапсулировать логику отправки запросов, 
    // который используется в GenericRepo
    {
        protected async Task<TResult> MakeJsonTypeRequest<TResult, TArg>(string relativePath, TArg arg,
           Func<string, TArg, JsonSerializerSettings?,CancellationToken , Task<HttpResponseMessage>> AsyncMethod)
            // здесь используется также операция отправки запроса, которая передается в аргументы, чтобы меньше писать код
        {
            var path = API_URL + relativePath;

            var response = await AsyncMethod(path, arg, null, default);

            await response.StatusIsOKOrThrowException(path);

            var result = await response.Content.ReadJsonByNewtonsoft<TResult>() ?? throw new NullReferenceException();

            return result;
        }

        public virtual async Task<TResult> MakePostJsonTypeRequest<TResult, TArg>(string relativePath, string token, TArg arg)
        {
            var client = GetClientWithBearer(token);

            return await MakeJsonTypeRequest<TResult, TArg>(relativePath, arg, client.PostAsJsonAsyncNewtonsoft);
        }
        public virtual async Task<TResult> MakePutJsonTypeRequest<TResult, TArg>(string relativePath, string token, TArg arg)
        {
            var client = GetClientWithBearer(token);
            

            return await MakeJsonTypeRequest<TResult, TArg>(relativePath, arg, client.PutAsJsonAsyncNewtonsoft);
        }
    }
}
