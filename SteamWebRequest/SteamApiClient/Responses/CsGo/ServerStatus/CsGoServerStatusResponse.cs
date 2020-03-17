using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.CsGo
{
    [Serializable]
    public sealed class CsGoServerStatusResponse : ApiResponse
    {
        [JsonProperty("result")]
        public CsGoServerStatus Contents { get; set; }
    }
}