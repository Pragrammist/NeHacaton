namespace Web.Dtos.UserSelfInfoDto.Profile
{
    public class OutputEmployeeSelfInfoDto
    {
        public string Fio { get; set; }
        public string ShortFio { get; set; }
        public List<OutputContactSelfInfoDto> Contacts { get; set; }
        public List<OutputPointSelfInfoDto> Points { get; set; }
        public OutputHumanSelfInfoDto Human { get; set; }
        public OutputWageProfileDto Wage { get; set; }
        public OutputUserProfileDto User { get; set; }
        public bool IsDirector { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsClient { get; set; }
        public bool IsAdmin { get; set; }
        public string Address { get; set; }
    }
}
