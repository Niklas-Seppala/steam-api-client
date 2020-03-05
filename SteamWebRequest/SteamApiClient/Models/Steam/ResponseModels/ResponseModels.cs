using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Steam.ResponseModels
{
    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class AccountCollectionContent
    {
        [JsonProperty("players")]
        public IReadOnlyList<SteamAccount> Accounts { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class AccountCollectionResponse
    {
        [JsonProperty("response")]
        public AccountCollectionContent Content { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class ApiListContent
    {
        public IReadOnlyList<ApiInterface> Interfaces { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class ApiListResponse
    {
        [JsonProperty("apilist")]
        public ApiListContent Apilist { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal class AppNewsResponse
    {
        public AppNewsCollection AppNews { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class FriendslistResponse
    {
        [JsonProperty("friendslist")]
        public FriendslistContent Content { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class FriendslistContent
    {
        public IReadOnlyList<Friend> Friends { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal class ProductContainer
    {
        [JsonProperty("apps")]
        public List<SteamProduct> ProductList { get; set; }

        [JsonProperty("have_more_results")]
        public bool MoreResults { get; set; }

        [JsonProperty("last_appid")]
        public ulong LastId { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal class SteamProductsContainer
    {
        [JsonProperty("response")]
        public ProductContainer Content { get; set; }
    }
}
