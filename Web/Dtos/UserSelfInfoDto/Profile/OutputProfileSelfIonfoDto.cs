using Newtonsoft.Json;

namespace Web.Dtos.UserSelfInfoDto.Profile
{
    public class OutputProfileDto
    {
        public int Male { get; set; }
        public DateTime? Birthday { get; set; }
        public string ShortFio { get; set; } = null!;
        public string Fio { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public bool IsDirector { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsClient { get; set; }
    }
}
