namespace Web.Dtos.UserSelfInfoDto.Rent
{
    public class OutputRentDto
    {
        public int Id { get; set; }
        public OutputClientDto Client { get; set; }
        public OutputAdminDto Admin { get; set; }
        public OutputDepositDto Deposit { get; set; }
        public List<OutputInventoryDto> Inventories { get; set; }
        public List<OutputProductDto> Products { get; set; }
        public List<OutputAdditionalServiceDto> AdditionalServices { get; set; }
        public List<OutputDiscountDto> Discounts { get; set; }
        public List<OutputPaymentDto> Payments { get; set; }
        public OutputRentStateDto RentState { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime TimeFactEnd { get; set; }
        public int SumAdditionalService { get; set; }
        public int SumDiscount { get; set; }
        public int SumProduct { get; set; }
        public int SumReal { get; set; }
        public int SumRental { get; set; }
        public int SumTotal { get; set; }
        public int Sum { get; set; }
        public int Payed { get; set; }
        public int OrderNumber { get; set; }
        public string OrderNumberText { get; set; }
        public bool NeedCalcBreaking { get; set; }
        public bool CeedCalcRentDelay { get; set; }
    }
}
