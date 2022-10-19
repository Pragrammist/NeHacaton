using Newtonsoft.Json;

namespace HendInRentApi.Dto.SelfInfo.Rent
{
    public class OutputInventoryDto
    {
        public int Id { get; set; }
        [JsonProperty("inventory_id")]
        public int InventoryId { get; set; }
        [JsonProperty("time_end")]
        public DateTime TimeEnd { get; set; }
        [JsonProperty("time_start")]
        public DateTime TimeStart { get; set; }
        public string Sum { get; set; }
        [JsonProperty("price_id")]
        public int PriceId { get; set; }
        [JsonProperty("sum_inventory")]
        public string SumInventory { get; set; }
        [JsonProperty("sum_broken")]
        public string SumBroken { get; set; }
        [JsonProperty("sum_service")]
        public string SumService { get; set; }
        public bool Finished { get; set; }
        [JsonProperty("sum_amount_payment")]
        public string SumAmountPayment { get; set; }
        [JsonProperty("sum_one")]
        public string SumOne { get; set; }
        public int Count { get; set; }
        [JsonProperty("kit_id")]
        public int KitId { get; set; }
        [JsonProperty("parent_inventory_id")]
        public int ParentInventoryId { get; set; }
        [JsonProperty("DiscountId")]
        public int DiscountId { get; set; }
        [JsonProperty("sum_discount")]
        public string SumDiscount { get; set; }
        [JsonProperty("sum_total")]
        public string SumTotal { get; set; }
        [JsonProperty("resource_kit_id")]
        public int ResourceKitId { get; set; }
        [JsonProperty("kit_number")]
        public int KitNumber { get; set; }
        public OutputInnerInventoryDto Inventory { get; set; }
        //public List<object> Services { get; set; } TODO
        //public List<object> Brokens { get; set; } TODO
        public OutputPriceDto Price { get; set; }
    }
}
