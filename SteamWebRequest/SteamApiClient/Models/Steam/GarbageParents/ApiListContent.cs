using System.Collections.Generic;

namespace SteamApiClient.Models
{
    internal class ApiListContent
    {
        public IReadOnlyCollection<ApiInterface> Interfaces { get; set; }
    }
}
