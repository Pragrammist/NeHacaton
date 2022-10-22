namespace HendInRentApi
{

    public class GenericRepositoryApi<TResult, TArg> : BaseRepository, HIRARepository<TResult, TArg>
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
        /// 
        public async Task<TResult> MakePostJsonTypeRequest(string relativePath, string token, TArg? arg)
        {
            return await base.MakePostJsonTypeRequest<TResult, TArg>(relativePath, token, arg);
        }

       

        public async Task<TResult> MakePutJsonTypeRequest(string relativePath, string token, TArg? arg)
        {
            return await base.MakePutJsonTypeRequest<TResult, TArg>(relativePath, token, arg);
        }
    }

    public class GenericRepositoryApi<TResult> : BaseRepository, HIRARepository<TResult>
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
        /// 

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
