namespace Web.Dtos.UserSelfInfoDto.Rent
{
    public class OutputUserRentSelfInfoDto
    {
        public List<OutputRentDto> Rents { get; set; }
        public string Message { get; set; }
        public OutputPermissionDto Permission { get; set; }
    }
}
