﻿using Newtonsoft.Json;

namespace HendInRentApi.Dto.SelfInfo.Profile
{
    public class OutputProfileSelfIonfoDto
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patro { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        public int Male { get; set; }
        public string Birthday { get; set; }
        [JsonProperty("short_fio")]
        public string ShortFio { get; set; }
        [JsonProperty("avatar_fio")]
        public string AvatarFio { get; set; }
        public string Fio { get; set; }
        public string Avatar { get; set; }
        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }
        [JsonProperty("is_director")]
        public bool IsDirector { get; set; }
        [JsonProperty("is_employee")]
        public bool IsEmployee { get; set; }
        [JsonProperty("is_client")]
        public bool IsClient { get; set; }
    }
}