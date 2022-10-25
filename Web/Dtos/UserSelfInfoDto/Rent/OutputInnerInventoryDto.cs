using Newtonsoft.Json;

namespace Web.Dtos.UserSelfInfoDto.Rent
{
    public class OutputInnerInventoryDto
    {
        public DateTime UpdatedAt { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Artule { get; set; }
        public int RentNumber { get; set; }
        public int Hidden { get; set; }
        public int Another { get; set; }
        public int AmountRentSum { get; set; }
        public string AmountRentTime { get; set; } = null!;
        public int ChildrenCount { get; set; }        
        public int AmountRentCount { get; set; }
        public int ExpenseSum { get; set; }
        public int CashDeposit { get; set; }
        public string Avatar { get; set; } = null!;
        public List<OutputPriceDto> Prices { get; set; } = new List<OutputPriceDto>();
        public bool IsGroup { get; set; }
        public OutputStateDto State { get; set; } = null!;
        public OutputPointDto Point { get; set; } = null!;
        public OutputResourceDto? Resource { get; set; }
    }
}
