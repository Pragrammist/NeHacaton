namespace HendInRentApi.Dto.Inventory   
{
    public class OutputInventoriesResultDto
    {
        public List<OutputInventoryDto> Array { get; set; }
        public string Message { get; set; }
        public OutputOptionDto Option { get; set; }
        public OutputPermissionDto Permission { get; set; }
    }
}
