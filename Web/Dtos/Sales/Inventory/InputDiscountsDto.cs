using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Web.Dtos.Sales.Inventory
{
    public class InputDiscountsDto
    {
        public InputDiscountDto? Discount { get; set; }
        public int? DiscountId { get; set; }
        public int? ResourceId { get; set; }
    }
}
