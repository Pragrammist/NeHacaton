using System.Net.Http.Json;
using static HendInRentApi.RentInHendApiConstants;
using static HendInRentApi.HttpStaticMethod;
using Newtonsoft.Json;

namespace HendInRentApi
{
    public class BaseMethodsApi
    {

        protected async Task<TResult> MakeJsonTypeRequest<TResult, TArg>(string relativePath, TArg arg,
           Func<string, TArg, JsonSerializerSettings?,CancellationToken , Task<HttpResponseMessage>> AsyncMethod)
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

            return await MakeJsonTypeRequest<TResult, TArg>(relativePath, arg, client.PostAsJsonAsyncByNewtonsoft);
        }
        public virtual async Task<TResult> MakePutJsonTypeRequest<TResult, TArg>(string relativePath, string token, TArg arg)
        {
            var client = GetClientWithBearer(token);

            return await MakeJsonTypeRequest<TResult, TArg>(relativePath, arg, client.PutAsJsonAsyncByNewtonsoft);
        }
    }
}
