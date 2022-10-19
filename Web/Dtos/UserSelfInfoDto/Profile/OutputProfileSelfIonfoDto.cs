using Newtonsoft.Json;

namespace Web.Dtos.UserSelfInfoDto.Profile
{
    public class OutputProfileSelfIonfoDto
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patro { get; set; }
        public int UserId { get; set; }
        public int Male { get; set; }
        public string Birthday { get; set; }
        public string ShortFio { get; set; }
        public string AvatarFio { get; set; }
        public string Fio { get; set; }
        public string Avatar { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDirector { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsClient { get; set; }
    }
}
