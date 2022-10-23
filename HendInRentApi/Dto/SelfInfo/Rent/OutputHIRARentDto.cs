using Newtonsoft.Json;

namespace HendInRentApi.Dto.SelfInfo.Rent
{
    public class OutputHIRARentDto
    {
        public int Id { get; set; }

        [JsonProperty("time_start")]
        public DateTime TimeStart { get; set; }

        [JsonProperty("time_end")]
        public DateTime TimeEnd { get; set; }

        [JsonProperty("time_fact_end")]
        public DateTime TimeFactEnd { get; set; }

        public int Sum { get; set; }

        public int Payed { get; set; }

        [JsonProperty("fine_time")]
        public int FineTime { get; set; }

        [JsonProperty("fine_broken")]
        public int FineBroken { get; set; }

        public string? Comment { get; set; }

        [JsonProperty("deposit_id")]
        public int DepositId { get; set; }

        [JsonProperty("sum_real")]
        public int SumReal { get; set; }

        [JsonProperty("sum_discount")]
        public int SumDiscount { get; set; }

        [JsonProperty("human_id")]
        public int HumanId { get; set; }

        [JsonProperty("order_number")]
        public int OrderNumber { get; set; }

        [JsonProperty("order_number_text")]
        public int OrderNumberText { get; set; }

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
        public int SumRental { get; set; }

        [JsonProperty("sum_product")]
        public int SumProduct { get; set; }

        [JsonProperty("sum_additional_service")]
        public int SumAdditionalService { get; set; }

        [JsonProperty("sum_total")]
        public int SumTotal { get; set; }

        [JsonProperty("sum_deposit")]
        public int SumDeposit { get; set; }

        [JsonProperty("is_understaffed")]
        public bool IsUnderstaffed { get; set; }

        [JsonProperty("google_event_id")]
        public int GoogleEventId { get; set; }

        public string Title { get; set; } = null!;

        [JsonProperty("rent_state")]
        public OutputHIRARentStateDto RentState { get; set; } = null!;

        public List<OutputHIRAInventoryDto> Inventories { get; set; } = new List<OutputHIRAInventoryDto>();        

        public OutputHIRAAdminDto Admin { get; set; } = null!;
    }
}
