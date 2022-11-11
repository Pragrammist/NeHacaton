namespace Web.Models
{
    public class UserChangeCityModel
    {
        public string City { get; set; } = null!;
        public string Login { get; set; } = null!;
    }

    public class UserChangeCityByLatLonModel
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Login { get; set; } = null!;
    }
}
