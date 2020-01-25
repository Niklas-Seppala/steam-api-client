using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public class ApiList
    {
        [JsonProperty("apilist")]
        public ApiListContent Apilist { get; set; }

        public List<Interface> Interfaces => this.Apilist.Interfaces;
    }
}
