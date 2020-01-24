using System.Collections.Specialized;

namespace SteamWebRequest
{
    public sealed class BarracksStatus
    {
        private readonly BitVector32 _bits;

        public bool BottomRanged => _bits[32];
        public bool BottomMelee => _bits[16];
        public bool MiddleRanged => _bits[8];
        public bool MiddleMelee => _bits[4];
        public bool TopRanged => _bits[2];
        public bool TopMelee => _bits[1];

        public BarracksStatus(BitVector32 bits)
        {
            _bits = bits;
        }
    }
}
