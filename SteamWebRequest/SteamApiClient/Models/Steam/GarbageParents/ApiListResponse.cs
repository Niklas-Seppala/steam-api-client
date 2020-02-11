using Newtonsoft.Json;

namespace SteamApi.Models
{
    internal class ApiListResponse
    {
        [JsonProperty("apilist")]
        public ApiListContent Apilist { get; set; }
    }
}
