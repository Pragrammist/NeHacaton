namespace Web.Dtos.UserSelfInfoDto.Rent
{
    public class OutputPriceDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int PointId { get; set; }
        public string? Article { get; set; }
        public List<OutputValueDto> Values { get; set; } = new List<OutputValueDto>();
    }
}
