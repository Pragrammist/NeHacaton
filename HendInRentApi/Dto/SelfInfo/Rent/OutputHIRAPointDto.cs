namespace HendInRentApi.Dto.SelfInfo.Rent
{
    public class OutputHIRAPointDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }
        public int PlaceId { get; set; }
        public int PlaceText { get; set; }
    }
}
