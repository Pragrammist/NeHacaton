namespace Web.Dtos.Sales.Service
{
    public class InputSearchServiceDto
    {
        public int? Id { get; set; }
        public int? Title { get; set; }
        public int? Price { get; set; }
        public InputSearchDiscountsDto? Discounts { get; set; }
        public string UserToken { get; set; } = null!;

    }
}
