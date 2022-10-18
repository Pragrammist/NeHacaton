using Newtonsoft.Json;

namespace Web.Dtos.UserSelfInfoDto
{
    public class InputRentSerchDto
    {
        [JsonProperty("search")]
        public string Search { get; set; }
        
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("close_point_id")]
        public int ClosePointId { get; set; }
        
        [JsonProperty("human_id")]
        public int HumanId { get; set; }
        
        [JsonProperty("deposit_id")]
        public int DepositId { get; set; }
        
        [JsonProperty("open_point_id")]
        public int OpenPointId { get; set; }
        
        [JsonProperty("rent_state_id")]
        public int RentStateId { get; set; }
        
        [JsonProperty("comment")]
        public string Comment { get; set; }
        
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        
        [JsonProperty("time_start")]
        public DateTime TimeStart { get; set; }
        
        [JsonProperty("time_end")]
        public DateTime TimeEnd { get; set; }
        
        [JsonProperty("time_fact_end")]
        public DateTime TimeFactEnd { get; set; }
        
        [JsonProperty("order_number_text")]
        public string OrderNumberText { get; set; }
        
        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("user_token")]
        public string UserToken { get; set; }
    }
}
