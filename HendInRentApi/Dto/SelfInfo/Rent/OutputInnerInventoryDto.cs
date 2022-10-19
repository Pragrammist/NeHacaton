using Newtonsoft.Json;

namespace HendInRentApi.Dto.SelfInfo.Rent
{
    public class OutputInnerInventoryDto
    {
        public int Id { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [JsonProperty("state_id")]
        public int StateId { get; set; }
        [JsonProperty("point_id")]
        public int PointId { get; set; }
        public string Artule { get; set; }
        [JsonProperty("category_resource_id")]
        public int CategoryResourceId { get; set; }
        [JsonProperty("rent_number")]
        public int RentNumber { get; set; }
        public int Hidden { get; set; }
        public int Another { get; set; }
        [JsonProperty("amount_rent_sum")]
        public string AmountRentSum { get; set; }
        [JsonProperty("amount_rent_time")]
        public string AmountRentTime { get; set; }
        [JsonProperty("parent_id")]
        public int ParentId { get; set; }
        [JsonProperty("children_count")]
        public int ChildrenCount { get; set; }
        [JsonProperty("amount_rent_count")]
        public int AmountRentCount { get; set; }
        [JsonProperty("expense_sum")]
        public string ExpenseSum { get; set; }
        [JsonProperty("cash_deposit")]
        public int CashDeposit { get; set; }
        public string Avatar { get; set; }
        public List<OutputPriceDto> Prices { get; set; }
        [JsonProperty("is_group")]
        public bool IsGroup { get; set; }
        public OutputStateDto State { get; set; }
        public OutputPointDto Point { get; set; }
        public OutputResourceDto Resource { get; set; }
    }
}
