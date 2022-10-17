namespace Web.Dtos.UserSelfInfoDto.Rent
{
    public class OutputInventoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RentNumber { get; set; }
        public int BuyPrice { get; set; }
        public string BuyDate { get; set; }
        public string Option { get; set; }
        public OutputStateDto State { get; set; }
        public OutputPointDto Point { get; set; }
        public OutputPriceDto Price { get; set; }
        public OutputDiscountsDto Discounts { get; set; }
    }
}
