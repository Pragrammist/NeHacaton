namespace Web.Dtos.Sales.Inventory
{
    public class OutputInventoriesResultDto
    {
        public List<OutputInventoryDto> Array { get; set; } = new List<OutputInventoryDto>();
        public string? Message { get; set; }
        public OutputOptionDto Option { get; set; } = null!;
        public OutputPermissionDto Permission { get; set; } = null!;
    }
}
