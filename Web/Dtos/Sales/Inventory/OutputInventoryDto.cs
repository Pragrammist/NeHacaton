using Newtonsoft.Json;

namespace Web.Dtos.Sales.Inventory
{
    public class OutputInventoryDto
    {
        public DateTime UpdatedAt { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Artule { get; set; }
        public int RentNumber { get; set; }
        public int Hidden { get; set; }
        public int Another { get; set; }
        public int AmountRentSum { get; set; }
        public string? AmountRentTime { get; set; }
        public int ChildrenCount { get; set; }
        public int AmountRentCount { get; set; }
        public int ExpenseSum { get; set; }
        public string Avatar { get; set; } = null!;
        public List<OutputPriceDto> Prices { get; set; } = new List<OutputPriceDto>();
        public bool IsGroup { get; set; }
        public OutputStateDto? State { get; set; }
        public OutputPointDto Point { get; set; } = null!;
        public OutputResourceDto? Resource { get; set; }
    }
}
