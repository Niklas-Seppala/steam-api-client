using System;

namespace SteamApi.Responses
{
    public interface IApiResponse
    {
        bool Successful { get; set; }

        bool WasCancelled { get; set; }

        string URL { get; set; }

        Exception ThrownException { get; set; }
    }
}
