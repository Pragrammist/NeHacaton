namespace HendInRentApi.Dto.SelfInfo.Rent
{
    public class OutputHIRARentsResultDto
    {
        public List<OutputHIRARentDto> Array { get; set; } = new List<OutputHIRARentDto>();
        public string? Message { get; set; }
        public OutputHIRAOptionDto? Option { get; set; }
        public OutputHIRAPermissionDto Permission { get; set; } = null!;
    }
}
