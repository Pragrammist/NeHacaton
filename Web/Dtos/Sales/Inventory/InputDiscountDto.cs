using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Web.Dtos.Sales.Inventory
{
    public class InputDiscountDto
    {
        public int? Id { get; set; }    
        public int? Title { get; set; }
        public int? Price { get; set; }
    } 
}
