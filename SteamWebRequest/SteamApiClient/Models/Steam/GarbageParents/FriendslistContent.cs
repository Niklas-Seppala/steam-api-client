using System.Collections.Generic;

namespace SteamApiClient.Models
{
    internal sealed class FriendslistContent
    {
        public IReadOnlyCollection<Friend> Friends { get; set; }
    }
}
