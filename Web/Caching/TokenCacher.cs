using Web.Dtos.Caching;

namespace Web.Caching
{
    public interface TokenCacher
    {
        string Cache(InputTokenCacheDto data, Func<string> dataSource);
        string CacheAsync(InputTokenCacheDto data, Func<Task<string>> dataSource);
    }
}
