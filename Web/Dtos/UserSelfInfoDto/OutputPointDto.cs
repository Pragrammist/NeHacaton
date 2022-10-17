namespace Web.Dtos.UserSelfInfoDto
{
    public class OutputPointDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Article { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string PlaceText { get; set; }
        public OutputPlaceDto Place { get; set; }
    }
}
