namespace HendInRentApi.Dto.SelfInfo.Profile
{
    public class OutputHIRAProfileSelfInfoResultDto
    {
        public List<OutputHIRAProfileSelfIonfoDto> Array { get; set; } = new List<OutputHIRAProfileSelfIonfoDto>();
        public string? Message { get; set; }
        public OutputHIRAPermissionSelfInfoDto Permission { get; set; } = null!;
    }
}
