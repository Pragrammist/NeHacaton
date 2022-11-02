using Microsoft.Extensions.Caching.Memory;

namespace Web.Caching
{
    public abstract class BaseCacher<TObject> : Cacher<TObject>
    {
        protected abstract TimeSpan Absolute { get; }
        protected abstract TimeSpan Sliding { get; }

        readonly protected IMemoryCache _cache;

        readonly protected string _defaultKey;

        public BaseCacher(IMemoryCache cache)
        {
            _cache = cache;
            _defaultKey = nameof(TObject);


        }


        public virtual IEnumerable<TObject> Cache(Func<IEnumerable<TObject>> dataSource) =>
            _cache.GetOrCreate(
            key: _defaultKey,
            factory: (opt) => DefaulFactory(opt, dataSource));



        public virtual IEnumerable<TObject> Cache(object key, Func<IEnumerable<TObject>> dataSource) => _cache.GetOrCreate(
            key,
            factory: (opt) => DefaulFactory(opt, dataSource));


        public async virtual Task<IEnumerable<TObject>> CacheAsync(Func<IAsyncEnumerable<TObject>> dataSource) =>
            await _cache.GetOrCreateAsync(
            _defaultKey,
            async opt => await DefaultFactoryAsync(opt, dataSource));


        public async virtual Task<IEnumerable<TObject>> CacheAsync(object key, Func<IAsyncEnumerable<TObject>> dataSource) =>
            await _cache.GetOrCreateAsync(
            key,
            async opt => await DefaultFactoryAsync(opt, dataSource));



        #region helpers
        void SetUserOptions(ICacheEntry options)
        {
            options.SetAbsoluteExpiration(Absolute);
            options.SetSlidingExpiration(Sliding);
            options.SetSize(1);
        }

        IEnumerable<TObject> DefaulFactory(ICacheEntry opt, Func<IEnumerable<TObject>> dataSource)
        {

            var users = dataSource().ToArray();
            SetUserOptions(opt);
            return users;
        }


        async Task<IEnumerable<TObject>> DefaultFactoryAsync(ICacheEntry opt, Func<IAsyncEnumerable<TObject>> dataSource)
        {
            var users = await dataSource().ToArrayAsync();
            SetUserOptions(opt);
            return users;
        }

        #endregion
    }
}
