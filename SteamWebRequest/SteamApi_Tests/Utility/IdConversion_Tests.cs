using SteamApi;
using System;
using Xunit;

namespace Utility
{
    public class IdConversion_Tests
    {
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
            Assert.Throws<ArgumentException>(() =>
            {
                SteamIdConverter.SteamIdTo64("-46773221");
            });
        }

        [Theory]
        [InlineData("147169892", "76561198107435620")]
        [InlineData("30339771", "76561197990605499")]
        [InlineData("46773221", "76561198007038949")]
        [InlineData("30672625", "76561197990938353")]
        public void SteamIdTo64_CorrectInput_ReturnsTrueAndValue(string id, string answer)
        {
            Assert.Matches(answer, SteamIdConverter.SteamIdTo64(id));
        }

        [Theory]
        [InlineData(147169892, 76561198107435620)]
        [InlineData(30339771, 76561197990605499)]
        [InlineData(46773221, 76561198007038949)]
        public void SteamIdTo64Int64Arg_ValidIdInput_ReturnsCorrectResult(uint id, ulong answer)
        {
            Assert.Equal(answer, SteamIdConverter.SteamIdTo64(id));
        }

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
            Assert.Throws<ArgumentException>(() =>
            {
                SteamIdConverter.SteamIdTo32("-76561198107435620");
            });
        }

        [Fact]
        public void SteamIdTo32_IdSmallerThanDifference_ThrowsArgumenOutOfRangeException()
        {
            Assert.Throws<OverflowException>(() =>
            {
                SteamIdConverter.SteamIdTo32("76561197960265726");
            });
        }

        [Theory]
        [InlineData("76561198107435620", "147169892")]
        [InlineData("76561197990605499", "30339771")]
        [InlineData("76561198007038949", "46773221")]
        [InlineData("76561197990938353", "30672625")]
        public void SteamIdTo32_ValidInput_ReturnsTrueAndValue(string id, string answer)
        {
            Assert.Matches(answer, SteamIdConverter.SteamIdTo32(id));
        }

        [Theory]
        [InlineData(76561197960265726)]
        [InlineData(76561197960265)]
        [InlineData(765611)]
        public void SteamIdTo32Int32_TooSmallIdInput_ThrowsArgumentOutOfRangeException(ulong id)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                SteamIdConverter.SteamIdTo32(id);
            });
        }

        [Theory]
        [InlineData(76561198107435620, 147169892)]
        [InlineData(76561197990605499, 30339771)]
        [InlineData(76561198007038949, 46773221)]
        [InlineData(76561197990938353, 30672625)]
        public void SteamIdTo32Int32_ValidIdInput_ReturnsCorrectResult(ulong id, uint answer)
        {
            Assert.Equal(answer, SteamIdConverter.SteamIdTo32(id));
        }
    }
}
