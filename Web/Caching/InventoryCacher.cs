using Microsoft.Extensions.Caching.Memory;
using Web.Dtos.Sales.Inventory;

namespace Web.Caching
{
    public class InventoryCacher : BaseCacher<OutputInventoryDto, string>
    {
        protected override TimeSpan Absolute => TimeSpan.FromMinutes(3);
        protected override TimeSpan Sliding => TimeSpan.FromSeconds(30);

        public InventoryCacher(IMemoryCache cache) : base(cache)
        {

        }
    }
}
