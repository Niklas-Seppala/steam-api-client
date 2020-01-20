using System.Collections.Specialized;

namespace SteamWebRequest.Models
{
    public readonly struct BarracksStatus
    {
        private readonly BitVector32 _bits;

        public bool BottomRanged { get => _bits[32]; }
        public bool BottomMelee { get => _bits[16]; }
        public bool MiddleRanged { get => _bits[8]; }
        public bool MiddleMelee { get => _bits[4]; }
        public bool TopRanged { get => _bits[2]; }
        public bool TopMelee { get => _bits[1]; }

        public BarracksStatus(BitVector32 bits)
        {
            _bits = bits;
        }
    }

}
