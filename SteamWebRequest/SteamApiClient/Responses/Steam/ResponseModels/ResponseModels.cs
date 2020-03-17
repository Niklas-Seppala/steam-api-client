using Newtonsoft.Json;
using System.Collections.Generic;


namespace SteamApi.Responses.Steam.ParentResponses
{
    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class AccountCollectionResponseParent
    {
        [JsonProperty("response")]
        public AccountCollectionContent Content { get; set; }
    }

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
    internal sealed class ApiListResponseParent
    {
        [JsonProperty("apilist")]
        public ApiListContent Apilist { get; set; }
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
    internal class AppNewsResponseParent
    {
        public AppNewsCollection AppNews { get; set; }
    }




    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class FriendslistResponseParent
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
    internal class SteamProductsResponseParent
    {
        [JsonProperty("response")]
        public ProductContainer Content { get; set; }
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
}
