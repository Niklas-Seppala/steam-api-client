using Newtonsoft.Json;

namespace SteamApiClient.Models
{
    internal class ApiListResponse
    {
        [JsonProperty("apilist")]
        public ApiListContent Apilist { get; set; }
    }
}
