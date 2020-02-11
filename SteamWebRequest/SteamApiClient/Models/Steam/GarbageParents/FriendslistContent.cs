using System.Collections.Generic;

namespace SteamApi.Models
{
    internal sealed class FriendslistContent
    {
        public IReadOnlyCollection<Friend> Friends { get; set; }
    }
}
