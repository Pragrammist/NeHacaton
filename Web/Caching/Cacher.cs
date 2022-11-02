namespace Web.Caching
{
    public interface Cacher<TObject, TKey>
    {
        IEnumerable<TObject> Cache(Func<IEnumerable<TObject>> dataSource);
        Task<IEnumerable<TObject>> CacheAsync(Func<IAsyncEnumerable<TObject>> dataSource);

        IEnumerable<TObject> Cache(TKey key, Func<IEnumerable<TObject>> dataSource);
        Task<IEnumerable<TObject>> CacheAsync(TKey key, Func<IAsyncEnumerable<TObject>> dataSource);

    }
}
