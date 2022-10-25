namespace Web.Dtos.Sales.Inventory
{
    public class OutputInventoriesResultDto
    {
        public List<OutputInventoryDto> Array { get; set; } = new List<OutputInventoryDto>();
        public string? Message { get; set; }
    }
}
