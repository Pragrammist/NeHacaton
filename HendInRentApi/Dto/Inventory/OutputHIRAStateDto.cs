using Newtonsoft.Json;

namespace HendInRentApi.Dto.Inventory
{
    public class OutputHIRAStateDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? @Const { get; set; }

        [JsonProperty("text_color")]
        public string? TextColor { get; set; }

        public string? Color { get; set; }
    }

}
