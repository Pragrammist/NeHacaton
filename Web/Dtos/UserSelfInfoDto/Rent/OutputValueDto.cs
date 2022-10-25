using Newtonsoft.Json;

namespace Web.Dtos.UserSelfInfoDto.Rent
{
    public class OutputValueDto
    {
        public string Period { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string Morethen { get; set; } = null!;
        public bool IsFixed { get; set; }
    }
}
