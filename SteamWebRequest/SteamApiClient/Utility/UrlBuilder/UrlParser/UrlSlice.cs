namespace SteamApi.Utility.Url
{
    /// <summary>
    /// Simple struct to encapsulate start and end
    /// indexes of the url slice.
    /// </summary>
    internal readonly struct UrlSlice
    {
        /// <summary>
        /// Start index
        /// </summary>
        public int Start { get; }

        /// <summary>
        /// End index
        /// </summary>
        public int End { get; }

        /// <summary>
        /// Slice length
        /// </summary>
        public int Length => End - Start;

        /// <summary>
        /// Instantiates UrlSlice object.
        /// </summary>
        /// <param name="start">start index</param>
        /// <param name="end">end index</param>
        public UrlSlice(int start, int end)
        {
            Start = start;
            End = end;
        }
    }
}
