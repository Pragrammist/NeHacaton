using Newtonsoft.Json;

namespace HendInRentApi.Dto.SelfInfo.Rent
{
    public class OutputHERAStateDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        [JsonProperty("text_color")]
        public string? TextColor { get; set; }
        public string? Color { get; set; }
        public string? @Const { get; set; }
    }
}
