namespace HendInRentApi.Dto.SelfInfo.Profile
{
    public class OutputHERAProfileSelfInfoResultDto
    {
        public List<OutputHERAProfileSelfIonfoDto> Array { get; set; } = new List<OutputHERAProfileSelfIonfoDto>();
        public string? Message { get; set; }
        public OutputHERAPermissionSelfInfoDto Permission { get; set; } = null!;
    }
}
