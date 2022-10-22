using Newtonsoft.Json;

namespace HendInRentApi.Dto.SelfInfo.Rent
{
    public class OutputHERAInnerInventoryDto
    {
        public int Id { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        [JsonProperty("state_id")]
        public int StateId { get; set; }
        [JsonProperty("point_id")]
        public int PointId { get; set; }
        public string? Artule { get; set; }
        [JsonProperty("category_resource_id")]
        public int CategoryResourceId { get; set; }
        [JsonProperty("rent_number")]
        public int RentNumber { get; set; }
        public int Hidden { get; set; }
        public int Another { get; set; }
        [JsonProperty("amount_rent_sum")]
        public int AmountRentSum { get; set; }
        [JsonProperty("amount_rent_time")]
        public string AmountRentTime { get; set; } = null!;
        [JsonProperty("parent_id")]
        public int ParentId { get; set; }
        [JsonProperty("children_count")]
        public int ChildrenCount { get; set; }
        [JsonProperty("amount_rent_count")]
        public int AmountRentCount { get; set; }
        [JsonProperty("expense_sum")]
        public int ExpenseSum { get; set; }
        [JsonProperty("cash_deposit")]
        public int CashDeposit { get; set; }
        public string Avatar { get; set; } = null!;
        public List<OutputHERAPriceDto> Prices { get; set; } = new List<OutputHERAPriceDto>();
        [JsonProperty("is_group")]
        public bool IsGroup { get; set; }
        public OutputHERAStateDto State { get; set; } = null!;
        public OutputHERAPointDto Point { get; set; } = null!;
        public OutputHERAResourceDto? Resource { get; set; }
    }
}
