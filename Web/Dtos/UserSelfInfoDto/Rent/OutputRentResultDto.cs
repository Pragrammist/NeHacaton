namespace Web.Dtos.UserSelfInfoDto.Rent
{
    public class OutputRentResultDto
    {
        public List<OutputRentDto> Array { get; set; }
        public string Message { get; set; }
        public OutputOptionDto Option { get; set; }
        public OutputPermissionDto permission { get; set; }
    }
}
