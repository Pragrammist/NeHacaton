namespace Web.Dtos.Sales.Inventory
{
    public class OutputInventoriesDto
    {
        public List<OutputInventoryDto> Array { get; set; } = new List<OutputInventoryDto>();
        public string? Message { get; set; }
        public OutputOptionDto? Option { get; set; }
        public OutputPermissionDto Permission { get; set; } = null!;
    }
}
