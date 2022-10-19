using System.Net.Http.Json;
using static HendInRentApi.RentInHendApiConstants;
using static HendInRentApi.HttpStaticMethod;

namespace HendInRentApi
{
    public class GenericRepositoryApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TArg"></typeparam>
        /// <param name="relativePath">start with '/'</param>
        /// <param name="arg"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TResult> MakePostJsonTypeRequest<TResult, TArg>(string relativePath, string token, TArg? arg)
        {
            var path = API_URL + relativePath; 

            var client = GetClientWithHeaders(token);

            var response = await client.PostAsJsonAsync(path, arg);

            await response.StatusIsOKOrThrowException(path);

            var result = await response.Content.ReadJsonByNewtonsoft<TResult>() ?? throw new NullReferenceException();

            return result;
        }

        public async Task<TResult> MakePostJsonTypeRequest<TResult>(string relativePath, string token)
        {
            var dummy = new { }; //chtobi vizvat method
            return await MakePostJsonTypeRequest<TResult, object>(relativePath, token, dummy);
        }

        public async Task<TResult> MakePutJsonTypeRequest<TResult, TDtoArg>(string relativePath, string token, TDtoArg arg)
        {
            var path = API_URL + relativePath;

            var client = GetClientWithHeaders(token);

            var response = await client.PutAsJsonAsync(path, arg);

            await response.StatusIsOKOrThrowException(path);

            var result = await response.Content.ReadJsonByNewtonsoft<TResult>() ?? throw new NullReferenceException();

            return result;
        }

        public async Task<TResult> MakePutJsonTypeRequest<TResult>(string relativePath, string token)
        {
            var dummy = new { }; //chtobi vizvat method
            return await MakePutJsonTypeRequest<TResult, object>(relativePath, token, dummy);
        }
    }


}
