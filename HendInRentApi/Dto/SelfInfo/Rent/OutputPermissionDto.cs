﻿using Newtonsoft.Json;

namespace HendInRentApi.Dto.SelfInfo.Rent
{
    public class OutputPermissionDto
    {
        [JsonProperty("resource_id")]
        public int ResourceId { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
        public bool Delete { get; set; }
        public bool Right { get; set; }
    }
}