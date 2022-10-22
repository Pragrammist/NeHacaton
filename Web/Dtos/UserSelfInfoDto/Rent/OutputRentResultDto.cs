namespace Web.Dtos.UserSelfInfoDto.Rent
{
    public class OutputRentResultDto
    {
        public List<OutputRentDto> Array { get; set; } = new List<OutputRentDto>();
        public string? Message { get; set; }
        public OutputOptionDto? Option { get; set; }
        public OutputPermissionDto Permission { get; set; } = null!;
    }
}
