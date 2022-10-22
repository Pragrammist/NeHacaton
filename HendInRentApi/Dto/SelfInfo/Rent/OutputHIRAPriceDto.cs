namespace HendInRentApi.Dto.SelfInfo.Rent
{
    public class OutputHIRAPriceDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int PointId { get; set; }
        public string? Article { get; set; }
        public List<OutputHIRAValueDto> Values { get; set; } = new List<OutputHIRAValueDto>();
    }
}
