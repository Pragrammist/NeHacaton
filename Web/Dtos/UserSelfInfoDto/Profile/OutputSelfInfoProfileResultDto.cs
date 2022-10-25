namespace Web.Dtos.UserSelfInfoDto.Profile
{
    public class OutputProfileResultDto
    {
        public List<OutputProfileDto> Array { get; set; } = new List<OutputProfileDto>();

        public OutputUserDto User { get; set; } = null!;
    }
}
