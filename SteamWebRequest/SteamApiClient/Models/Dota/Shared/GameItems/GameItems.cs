﻿using Newtonsoft.Json;

namespace SteamApiClient.Models
{
    internal class GameItems
    {
        [JsonProperty("result")]
        public ItemsContent Content { get; set; }
    }
}
