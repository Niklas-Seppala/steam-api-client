using System.Collections.Generic;

namespace SteamApi.Models
{
    internal sealed class FriendslistContent
    {
        public IReadOnlyList<Friend> Friends { get; set; }
    }
}
