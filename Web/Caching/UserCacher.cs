using DataBase.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Web.Caching
{
    public class UserCacher : Cacher<User>
    {
        TimeSpan Absolute => TimeSpan.FromMinutes(2);
        TimeSpan Sliding => TimeSpan.FromSeconds(30);

        readonly IMemoryCache _cache;

        readonly string _defaultKey;

        public UserCacher(IMemoryCache cache)
        {
            _cache = cache;
            _defaultKey = nameof(User);
            

        }

      
        public IEnumerable<User> Cache(Func<IEnumerable<User>> dataSource) =>
        _cache.GetOrCreate(
        key: _defaultKey,
        factory: (opt) => DefaulFactory(opt, dataSource));



        public IEnumerable<User> Cache(object key, Func<IEnumerable<User>> dataSource) => _cache.GetOrCreate(
            key,
            factory: (opt) => DefaulFactory(opt, dataSource));
 

        public async Task<IEnumerable<User>> CacheAsync(Func<IAsyncEnumerable<User>> dataSource) => 
            await _cache.GetOrCreateAsync(
            _defaultKey,
            async opt => await DefaultFactoryAsync(opt, dataSource));
        

        public async Task<IEnumerable<User>> CacheAsync(object key, Func<IAsyncEnumerable<User>> dataSource) =>
            await _cache.GetOrCreateAsync(
            key,
            async opt => await DefaultFactoryAsync(opt, dataSource));



        #region helpers
        void SetUserOptions(ICacheEntry options)
        {
            options.SetAbsoluteExpiration(Absolute);
            options.SetSlidingExpiration(Sliding);

        }

        IEnumerable<User> DefaulFactory(ICacheEntry opt, Func<IEnumerable<User>> dataSource)
        {

            var users = dataSource().ToArray();
            SetUserOptions(opt);
            return users;
        }


        async Task<IEnumerable<User>> DefaultFactoryAsync(ICacheEntry opt, Func<IAsyncEnumerable<User>> dataSource)
        {
            var users = await dataSource().ToArrayAsync();
            SetUserOptions(opt);
            return users;
        }
        #endregion
    }
}
