using System.Collections.Specialized;

namespace SteamWebRequest.Models
{
    public readonly struct TowerStatus
    {
        private readonly BitVector32 _bits;
        public bool AncientTop { get => _bits[1024]; }
        public bool AncientBottom { get => _bits[512]; }
        public bool BottomTier_3 { get => _bits[256]; }
        public bool BottomTier_2 { get => _bits[128]; }
        public bool BottomTier_1 { get => _bits[64]; }
        public bool MiddleTier_3 { get => _bits[32]; }
        public bool MiddleTier_2 { get => _bits[16]; }
        public bool MiddleTier_1 { get => _bits[8]; }
        public bool TopTier_3 { get => _bits[4]; }
        public bool TopTier_2 { get => _bits[2]; }
        public bool TopTier_1 { get => _bits[1]; }

        public TowerStatus(BitVector32 bits) => _bits = bits;
    }

}
