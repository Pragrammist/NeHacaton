namespace Web.Dtos.UserSelfInfoDto.Profile
{
    public class OutputProfileSelfInfoDto
    {
        public List<OutputEmployeeSelfInfoDto> Array { get; set; }
        public string Message { get; set; }
        public OutputPermissionSelfInfoDto Permission { get; set; }
    }
}
