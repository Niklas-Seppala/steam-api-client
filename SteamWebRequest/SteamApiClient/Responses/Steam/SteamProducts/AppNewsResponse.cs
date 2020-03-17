using System;

namespace SteamApi.Responses.Steam
{
    [Serializable]
    public class AppNewsResponse : ApiResponse
    {
        public AppNewsCollection Contents { get; set; }
    }
}
