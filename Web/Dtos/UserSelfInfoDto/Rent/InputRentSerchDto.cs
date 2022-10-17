namespace Web.Dtos.UserSelfInfoDto.Rent
{
    public class InputRentSerchDto
    {
        public string? Search { get; set; }
        public int? Id { get; set; }
        public int? ClosePointId { get; set; }
        public int? HumanId { get; set; }
        public int? DepositId { get; set; }
        public int? OpenPointId { get; set; }
        public int? RentStateId { get; set; }
        public string? Comment { get; set; }
        public DateTime?  CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public DateTime? TimeFactEnd { get; set; }
        public string? OrderNumberText { get; set; }
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public string UserToken { get; set; } = null!;
    }
}
