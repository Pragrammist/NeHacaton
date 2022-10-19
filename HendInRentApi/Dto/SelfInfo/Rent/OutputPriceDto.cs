namespace HendInRentApi.Dto.SelfInfo.Rent
{
    public class OutputPriceDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PointId { get; set; }
        public string Article { get; set; }
        public List<OutputValueDto> Values { get; set; }
    }
}
