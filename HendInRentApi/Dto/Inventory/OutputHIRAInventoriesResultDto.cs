namespace HendInRentApi.Dto.Inventory   
{
    public class OutputHIRAInventoriesResultDto
    {
        public List<OutputHIRAInventoryDto> Array { get; set; } = new List<OutputHIRAInventoryDto>();
        public string? Message { get; set; }
        public OutputHIRAOptionDto Option { get; set; } = null!;
        public OutputHIRAPermissionDto Permission { get; set; } = null!;
    }
}
