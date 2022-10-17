namespace Web.Dtos.UserSelfInfoDto
{
    public class OutputUserRentSelfInfoDto
    {
        public List<OutputRentDto> Rents { get; set; }
        public string Message { get; set; }
        public OutputPermissionDto Permission { get; set; }
    }
}
