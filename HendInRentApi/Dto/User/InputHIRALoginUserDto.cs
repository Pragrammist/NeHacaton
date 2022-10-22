using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HendInRentApi
{
    public class InputHIRALoginUserDto
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
