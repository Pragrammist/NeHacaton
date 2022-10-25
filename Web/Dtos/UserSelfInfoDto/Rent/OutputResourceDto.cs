using Newtonsoft.Json;

namespace Web.Dtos.UserSelfInfoDto.Rent
{
    public class OutputResourceDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Body { get; set; }
        public DateTime DeletedAt { get; set; }
        public string? @Const { get; set; }
    }
}
