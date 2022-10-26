using Newtonsoft.Json;

namespace Web.Dtos.Sales.Inventory
{
    public class InputSearchInventoryDto
    {
        public string? Search { get; set; }
        public string[]? Tags { get; set; }
        public string? City { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? RentNumber { get; set; }
        public int? StateId { get; set; }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public InputDiscountsDto? Discounts { get; set; }
    }
}
