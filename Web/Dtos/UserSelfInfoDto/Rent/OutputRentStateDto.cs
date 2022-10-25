using Newtonsoft.Json;

namespace Web.Dtos.UserSelfInfoDto.Rent
{
    public class OutputRentStateDto
    {
        public string Title { get; set; } = null!;
        public string? @Const { get; set; }
        public string? DomClass { get; set; }
    }
}
