using Newtonsoft.Json;

namespace Web.Dtos.UserSelfInfoDto.Rent
{
    public class OutputRentDto
    {
        public int Id { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime TimeFactEnd { get; set; }
        public int Sum { get; set; }
        public int Payed { get; set; }
        public int FineTime { get; set; }
        public int FineBroken { get; set; }
        public string? Comment { get; set; }
        public int SumReal { get; set; }
        public int SumDiscount { get; set; }
        public int OrderNumber { get; set; }
        public int OrderNumberText { get; set; }
        public int NeedCalcBreaking { get; set; }
        public int NeedCalcRentDelay { get; set; }
        public int SumRental { get; set; }
        public int SumProduct { get; set; }
        public int SumAdditionalService { get; set; }
        public int SumTotal { get; set; }
        public int SumDeposit { get; set; }
        public bool IsUnderstaffed { get; set; }
        public string Title { get; set; } = null!;
        public OutputRentStateDto RentState { get; set; } = null!;
        public List<OutputInventoryDto> Inventories { get; set; } = new List<OutputInventoryDto>();
        public OutputAdminDto Admin { get; set; } = null!;
    }
}
