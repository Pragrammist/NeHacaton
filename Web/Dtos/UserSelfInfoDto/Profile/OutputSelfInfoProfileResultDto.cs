namespace Web.Dtos.UserSelfInfoDto.Profile
{
    public class OutputSelfInfoProfileResultDto
    {
        public List<OutputProfileSelfIonfoDto> Array { get; set; }
        public string Message { get; set; }
        public OutputPermissionSelfInfoDto Permission { get; set; }
    }
}
