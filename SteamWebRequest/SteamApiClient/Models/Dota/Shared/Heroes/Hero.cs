﻿using Newtonsoft.Json;

namespace SteamApiClient.Models
{
    public class Hero
    {
        [JsonProperty("Localized_name")]
        public string LocalizedName { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}