using DataBase.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Web.Caching
{
    public class UserCacher : BaseCacher<User, string>
    {
        const string prefix = "us_";
        protected override TimeSpan Absolute => TimeSpan.FromMinutes(30);
        protected override TimeSpan Sliding => TimeSpan.FromSeconds(3);
        public UserCacher(IMemoryCache cache) : base(cache)
        {
           

        }

        public override IEnumerable<User> Cache(string city, Func<IEnumerable<User>> dataSource)
        {
            Key(ref city);
            return base.Cache(city, dataSource);
        }
        public override Task<IEnumerable<User>> CacheAsync(string city, Func<IAsyncEnumerable<User>> dataSource)
        {
            Key(ref city);
            return base.CacheAsync(city, dataSource);
        }
        void Key(ref string key) => key = prefix + key;
    }
}
