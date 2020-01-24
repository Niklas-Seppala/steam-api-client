namespace SteamApiClient
{
    /// <summary>
    /// Readonly Querystring parameter object.
    /// </summary>
    public readonly struct QueryParam
    {
        public string Key { get; }
        public string Value { get; }

        /// <summary>
        /// Instantiates QueryParam object.
        /// </summary>
        /// <param name="key">parameter key</param>
        /// <param name="value">parameter value</param>
        public QueryParam(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
