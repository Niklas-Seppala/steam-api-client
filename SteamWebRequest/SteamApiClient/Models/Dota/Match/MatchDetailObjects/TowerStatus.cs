using System.Collections.Specialized;

namespace SteamApi.Models.Dota
{
    public sealed class TowerStatus
    {
        private readonly BitVector32 _bits;
        public bool AncientTop => _bits[1024];
        public bool AncientBottom => _bits[512];
        public bool BottomTier_3 => _bits[256];
        public bool BottomTier_2 => _bits[128];
        public bool BottomTier_1 => _bits[64];
        public bool MiddleTier_3 => _bits[32];
        public bool MiddleTier_2 => _bits[16];
        public bool MiddleTier_1 => _bits[8];
        public bool TopTier_3 => _bits[4];
        public bool TopTier_2 => _bits[2];
        public bool TopTier_1 => _bits[1];

        public TowerStatus(BitVector32 bits)
        {
            _bits = bits;
        }
    }
}
