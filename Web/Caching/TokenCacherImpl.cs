using Web.Dtos.Caching;

namespace Web.Caching
{
    public class TokenCacherImpl : TokenCacher
    {
        public string Cache(InputTokenCacheDto data, Func<string> dataSource)
        {
            throw new NotImplementedException();
        }

        public string CacheAsync(InputTokenCacheDto data, Func<Task<string>> dataSource)
        {
            throw new NotImplementedException();
        }
    }
}
