using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HendInRentApi
{
    public class InputLoginUserRentInHendDto
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
