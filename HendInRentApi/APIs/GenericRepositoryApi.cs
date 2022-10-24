namespace HendInRentApi
{

    public class GenericRepositoryApi<TResult, TArg> : BaseMethodsApi, HIRARepository<TResult, TArg>
    {
        public async Task<TResult> MakePostJsonTypeRequest(string relativePath, string token, TArg? arg)
        {
            return await base.MakePostJsonTypeRequest<TResult, TArg>(relativePath, token, arg);
        }
        public async Task<TResult> MakePutJsonTypeRequest(string relativePath, string token, TArg? arg)
        {
            return await base.MakePutJsonTypeRequest<TResult, TArg>(relativePath, token, arg);
        }
    }

    public class GenericRepositoryApi<TResult> : BaseMethodsApi, HIRARepository<TResult>
    {
        public async Task<TResult> MakePostJsonTypeRequest(string relativePath, string token)
        {
            return await base.MakePostJsonTypeRequest<TResult,object>(relativePath, token, new {});
        }

        public async Task<TResult> MakePutJsonTypeRequest(string relativePath, string token)
        {
            return await base.MakePutJsonTypeRequest<TResult, object>(relativePath, token, new {});
        }

        
    }
}
