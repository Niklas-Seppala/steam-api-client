using System;

namespace SteamApi.Responses.Steam
{
    [Serializable]
    public class ProfileAvatarResponse : ApiResponse
    {
        public byte[] Contents { get; set; }
    }
}
