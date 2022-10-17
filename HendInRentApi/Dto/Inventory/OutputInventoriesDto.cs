namespace HendInRentApi
{
    public class OutputInventoriesDto
    {
        public List<OutputInventoryDto> Array { get; set; } = new List<OutputInventoryDto>();
        public string? Message { get; set; }
        public OutputPermissionDto Permission { get; set; } = new OutputPermissionDto();
    }    
}
