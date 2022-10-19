namespace HendInRentApi.Dto.SelfInfo.Profile
{
    public class OutputSelfInfoProfileApiResultDto
    {
        public List<OutputProfileSelfIonfoDto> Array { get; set; }
        public string Message { get; set; }
        public OutputPermissionSelfInfoDto Permission { get; set; }
    }
}
