using Newtonsoft.Json;

namespace HendInRentApi.Dto.SelfInfo.Rent
{
    public class OutputRentDto
    {
        public int Id { get; set; }
        [JsonProperty("time_start")]
        public DateTime TimeStart { get; set; }
        [JsonProperty("time_end")]
        public DateTime TimeEnd { get; set; }
        [JsonProperty("time_fact_end")]
        public DateTime TimeFactEnd { get; set; }
        public string Sum { get; set; }
        public string Payed { get; set; }
        [JsonProperty("fine_time")]
        public string FineTime { get; set; }
        [JsonProperty("fine_broken")]
        public string FineBroken { get; set; }
        public string Comment { get; set; }
        [JsonProperty("deposit_id")]
        public int DepositId { get; set; }
        [JsonProperty("sum_real")]
        public string SumReal { get; set; }
        [JsonProperty("sum_discount")]
        public string SumDiscount { get; set; }
        [JsonProperty("human_id")]
        public int HumanId { get; set; }
        [JsonProperty("order_number")]
        public int OrderNumber { get; set; }
        [JsonProperty("order_number_text")]
        public string OrderNumberText { get; set; }
        [JsonProperty("need_calc_breaking")]
        public int NeedCalcBreaking { get; set; }
        [JsonProperty("need_calc_rent_delay")]
        public int NeedCalcRentDelay { get; set; }
        [JsonProperty("open_point_id")]
        public int OpenPointId { get; set; }
        [JsonProperty("close_point_id")]
        public int ClosePointId { get; set; }
        [JsonProperty("rent_state_id")]
        public int RentStateId { get; set; }
        [JsonProperty("sum_rental")]
        public string SumRental { get; set; }
        [JsonProperty("sum_product")]
        public string SumProduct { get; set; }
        [JsonProperty("sum_additional_service")]
        public string SumAdditionalService { get; set; }
        [JsonProperty("sum_total")]
        public string SumTotal { get; set; }
        [JsonProperty("sum_deposit")]
        public string SumDeposit { get; set; }
        [JsonProperty("is_understaffed")]
        public bool IsUnderstaffed { get; set; }
        [JsonProperty("google_event_id")]
        public int GoogleEventId { get; set; }
        public string Title { get; set; }
        [JsonProperty("rent_state")]
        public OutputRentStateDto RentState { get; set; }
        public List<object> Payments { get; set; }
        public List<OutputInventoryDto> Inventories { get; set; }
        public List<object> Delivery { get; set; }
        public List<OutputDiscountDto> Discounts { get; set; } //i'm not sure
        public List<object> Kits { get; set; }
        [JsonProperty("additional_services")]
        public List<object> AdditionalServices { get; set; }
        public object Deposit { get; set; }
        public object Client { get; set; } // i don't know what is object must me here, cause in doc doesn't have
        public OutputAdminDto Admin { get; set; }
    }
}
