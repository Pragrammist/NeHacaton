﻿using Newtonsoft.Json;

namespace Web.Dtos.UserSelfInfoDto.Rent
{
    public class OutputRentStateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string @Const { get; set; }
        [JsonProperty("dom_class")]
        public string DomClass { get; set; }
    }
}
