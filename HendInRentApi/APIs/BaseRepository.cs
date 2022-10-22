using System.Net.Http.Json;
using static HendInRentApi.RentInHendApiConstants;
using static HendInRentApi.HttpStaticMethod;

namespace HendInRentApi
{
    public class BaseRepository
    {
        public virtual async Task<TResult> MakePostJsonTypeRequest<TResult, TArg>(string relativePath, string token, TArg? arg)
        {
            var path = API_URL + relativePath;

            var client = GetClientWithHeaders(token);

            var response = await client.PostAsJsonAsync(path, arg);

            await response.StatusIsOKOrThrowException(path);

            var result = await response.Content.ReadJsonByNewtonsoft<TResult>() ?? throw new NullReferenceException();

            return result;
        }
        public virtual async Task<TResult> MakePutJsonTypeRequest<TResult, TArg>(string relativePath, string token, TArg? arg)
        {
            var path = API_URL + relativePath;

            var client = GetClientWithHeaders(token);

            var response = await client.PutAsJsonAsync(path, arg);

            await response.StatusIsOKOrThrowException(path);

            var result = await response.Content.ReadJsonByNewtonsoft<TResult>() ?? throw new NullReferenceException();

            return result;
        }
    }
}
