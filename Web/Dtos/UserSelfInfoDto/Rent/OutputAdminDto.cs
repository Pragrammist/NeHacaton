using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Dtos.UserSelfInfoDto.Rent
{
    public class OutputAdminDto
    {
        public int Id { get; set; }
        public string Guid { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Patro { get; set; }
        public int UserId { get; set; }
        public int Male { get; set; }
        public DateTime? Birthday { get; set; }
        public string ShortFio { get; set; } = null!;
        public string AvatarFio { get; set; } = null!;
        public string Fio { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public bool IsDirector { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsClient { get; set; }
    }
}
