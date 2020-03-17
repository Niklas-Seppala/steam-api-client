using System;

namespace SteamApi.Responses
{
    public interface IApiResponse
    {
        bool Successful { get; set; }

        string URL { get; set; }

        Exception ThrownException { get; set; }
    }
}
