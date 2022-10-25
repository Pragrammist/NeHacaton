namespace Web.Dtos.UserSelfInfoDto.Profile
{
    public class OutputSelfInfoProfileResultDto
    {
        public List<OutputProfileSelfIonfoDto> Array { get; set; } = new List<OutputProfileSelfIonfoDto>();

        public OutputUserDto User { get; set; } = null!;
    }
}
