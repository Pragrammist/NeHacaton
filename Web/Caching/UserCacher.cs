using DataBase.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Web.Caching
{
    public class UserCacher : BaseCacher<User>
    {
        protected override TimeSpan Absolute => TimeSpan.FromMinutes(30);
        protected override TimeSpan Sliding => TimeSpan.FromSeconds(3);
        public UserCacher(IMemoryCache cache) : base(cache)
        {
           

        }

      
        
    }
}
