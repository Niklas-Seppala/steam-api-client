namespace SteamApi
{
    /// <summary>
    /// Size of the product request.
    /// </summary>
    public enum ProductCallSize
    {
        Small = 1000,
        Medium = 10000,
        Large = 25000,
        Max = 50000,
        All = -1,

        Default = Medium
    }
}
