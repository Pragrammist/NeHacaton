namespace Web.Dtos.UserSelfInfoDto
{
    public class OutputRentStateDto
    {
        public string Guid { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patro { get; set; }
        public string Fio { get; set; }
        public string Avatar { get; set; }
        public string Birthday { get; set; }
        public bool IsDirector { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsClient { get; set; }
        public bool IsAdmin { get; set; }
        public OutputDiscountsDto Discounts { get; set; }
    }
}
