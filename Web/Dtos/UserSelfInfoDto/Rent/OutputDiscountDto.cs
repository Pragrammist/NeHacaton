﻿using Newtonsoft.Json;

namespace Web.Dtos.UserSelfInfoDto.Rent
{
    public class OutputDiscountDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public int Title { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }
    }
}
