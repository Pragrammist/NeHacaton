using Newtonsoft.Json;

namespace Web.Dtos.UserSelfInfoDto.Rent
{
    public class OutputResourceDto
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Body { get; set; }
        public int DictId { get; set; }
        public int ControllerId { get; set; }
        [JsonProperty("deleted_at")]
        public DateTime DeletedAt { get; set; }
        public int OrderId { get; set; }
        public string? @Const { get; set; }
    }
}
