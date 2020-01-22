using System;
using Xunit;
using SteamWebRequest;

namespace SWR.SteamIdConverter_Tests
{
    public class SteamId64to32_Tests
    {
        // ------------------------------------------------------------------ \\
        //                      SteamIdTo32(string id64)                      \\
        // --------------------------- Wrong usage -------------------------- \\

        [Theory]
        [InlineData("")]
        [InlineData("12345666666.66")]
        [InlineData("0x5b3d3")]
        [InlineData("abcdefg")]
        public void SteamIdTo32_InvalidIntegerInput_ReturnsFalseAndEmptyString(string id)
        {
            Assert.Throws<ArgumentException>(() => SteamIdConverter.SteamIdTo32(id));
        }

        [Fact]
        public void SteamIdTo32_NegativeIdInput_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                SteamIdConverter.SteamIdTo32("-76561198107435620");
            });
        }

        [Fact]
        public void SteamIdTo32_IdSmallerThanDifference_ThrowsArgumenOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { 
                SteamIdConverter.SteamIdTo32("76561197960265726");
            });
        }

        // -------------------------- Correct usage ------------------------- \\

        [Theory]
        [InlineData("76561198107435620", "147169892")]
        [InlineData("76561197990605499", "30339771")]
        [InlineData("76561198007038949", "46773221")]
        [InlineData("76561197990938353", "30672625")]
        public void SteamIdTo32_ValidInput_ReturnsTrueAndValue(string id, string answer)
        {
            Assert.Matches(answer, SteamIdConverter.SteamIdTo32(id));
        }

        // ------------------------------------------------------------------ \\
        //                       SteamIdTo32(int id64)                        \\
        // --------------------------- Wrong usage -------------------------- \\

        [Theory]
        [InlineData(76561197960265726)]
        [InlineData(76561197960265)]
        [InlineData(765611)]
        public void SteamIdTo32Int32_TooSmallIdInput_ThrowsArgumentOutOfRangeException(ulong id)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                SteamIdConverter.SteamIdTo32(id);
            });
        }

        // -------------------------- Correct usage ------------------------- \\

        [Theory]
        [InlineData(76561198107435620, 147169892)]
        [InlineData(76561197990605499, 30339771)]
        [InlineData(76561198007038949, 46773221)]
        [InlineData(76561197990938353, 30672625)]
        public void SteamIdTo32Int32_ValidIdInput_ReturnsCorrectResult(ulong id, int answer)
        {
            Assert.Equal(answer, SteamIdConverter.SteamIdTo32(id));
        }
    }
}
