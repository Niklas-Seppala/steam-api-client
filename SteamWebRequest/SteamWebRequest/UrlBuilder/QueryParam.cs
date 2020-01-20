namespace SteamWebRequest
{
    public readonly struct QueryParam
    {
        public string Key { get; }
        public string Value { get; }

        public QueryParam(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
