namespace HendInRentApi.Dto.Inventory   
{
    public class OutputHERAInventoriesResultDto
    {
        public List<OutputHERAInventoryDto> Array { get; set; } = new List<OutputHERAInventoryDto>();
        public string? Message { get; set; }
        public OutputHERAOptionDto Option { get; set; } = null!;
        public OutputHERAPermissionDto Permission { get; set; } = null!;
    }
}
