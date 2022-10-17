namespace Web.Dtos.UserSelfInfoDto.Profile
{
    public class OutputUserProfileDto
    {
        public string Login { get; set; }
        public int Confirmed { get; set; }
        public DateTime LastVisit { get; set; }
        public DateTime Tokens { get; set; }
    }
}
