using System;
using Xunit;
using SteamWebRequest;

namespace SWR.SteamIdConverter_Tests
{
    public class SteamId32To64_Tests
    {    
        // ------------------------------------------------------------------ \\
        //                        SteamIdTo64(string id32)                    \\
        // -------------------------- Wrong usage --------------------------- \\

        [Theory]
        [InlineData("")]
        [InlineData("30339771.3")]
        [InlineData("0x5b3d3")]
        [InlineData("abcdefg")]
        public void SteamIdTo64_InvalidIntegerInput_ReturnsFalseAndEmptyString(string id)
        {
            Assert.Throws<ArgumentException>(() => SteamIdConverter.SteamIdTo64(id));
        }

        [Fact]
        public void SteamIdTo64_NegativeIdInput_ThrowsArgumenOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                SteamIdConverter.SteamIdTo64("-46773221"); 
            });
        }

        // -------------------------- Correct usage ------------------------- \\

        [Theory]
        [InlineData("147169892", "76561198107435620")]
        [InlineData("30339771", "76561197990605499")]
        [InlineData("46773221", "76561198007038949")]
        [InlineData("30672625", "76561197990938353")]
        public void SteamIdTo64_CorrectInput_ReturnsTrueAndValue(string id, string answer)
        {
            Assert.Matches(answer, SteamIdConverter.SteamIdTo64(id));
        }

        // ------------------------------------------------------------------ \\
        //                       SteamIdTo64(int id32)                        \\
        // -------------------------- Correct usage ------------------------- \\

        [Theory]
        [InlineData(147169892, 76561198107435620)]
        [InlineData(30339771, 76561197990605499)]
        [InlineData(46773221, 76561198007038949)]
        public void SteamIdTo64Int64Arg_ValidIdInput_ReturnsCorrectResult(uint id, long answer)
        {
            Assert.Equal(answer, SteamIdConverter.SteamIdTo64(id));
        }
    }
}
