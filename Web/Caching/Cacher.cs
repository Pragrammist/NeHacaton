﻿using DataBase.Entities;

namespace Web.Caching
{
    public interface Cacher<TObject>
    {
        IEnumerable<TObject> Cache(Func<IEnumerable<TObject>> dataSource);
        Task<IEnumerable<TObject>> CacheAsync(Func<IAsyncEnumerable<TObject>> dataSource);

        IEnumerable<TObject> Cache(object key, Func<IEnumerable<TObject>> dataSource);
        Task<IEnumerable<TObject>> CacheAsync(object key, Func<IAsyncEnumerable<TObject>> dataSource);

    }
}
