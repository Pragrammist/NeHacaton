namespace HendInRentApi.Dto.SelfInfo.Rent
{
    public class OutputHERARentsResultDto
    {
        public List<OutputHERARentDto> Array { get; set; } = new List<OutputHERARentDto>();
        public string? Message { get; set; }
        public OutputHERAOptionDto? Option { get; set; }
        public OutputHERAPermissionDto Permission { get; set; } = null!;
    }
}
