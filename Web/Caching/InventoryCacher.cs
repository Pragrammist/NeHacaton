using Microsoft.Extensions.Caching.Memory;
using Web.Dtos.Sales.Inventory;

namespace Web.Caching
{
    public class InventoryCacher : BaseCacher<OutputInventoryDto, string>
    {
        const string prefix = "inv_";
        protected override TimeSpan Absolute => TimeSpan.FromMinutes(3);
        protected override TimeSpan Sliding => TimeSpan.FromSeconds(30);

        public InventoryCacher(IMemoryCache cache) : base(cache)
        {

        }
        public override IEnumerable<OutputInventoryDto> Cache(string login, Func<IEnumerable<OutputInventoryDto>> dataSource)
        {
            Key(ref login);
            return base.Cache(login, dataSource);
        }
        public override Task<IEnumerable<OutputInventoryDto>> CacheAsync(string login, Func<IAsyncEnumerable<OutputInventoryDto>> dataSource)
        {
            Key(ref login);
            return base.CacheAsync(login, dataSource);
        }
        void Key(ref string key) => key = prefix + key;
    }
}
