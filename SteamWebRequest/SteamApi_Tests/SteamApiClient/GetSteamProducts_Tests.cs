using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SteamApi;

namespace Client
{
    public class GetSteamProducts_Tests : SteamApiClientTests
    {
        public GetSteamProducts_Tests(ClientFixture fixture) : base(fixture) {}


        /// <summary>
        /// Test case for all products requested. Method should return all
        /// products
        /// </summary>
        [Fact]
        public void CallSizeAll_ReturnsNoProducts()
        {
            var response = SteamApiClient.GetSteamProductsAsync(IncludeProducts.GameProducs, callSize: ProductCallSize.All)
                .Result;

            Assert.True(response.Count > (int)ProductCallSize.Max);
        }

        [Theory]
        [InlineData(ProductCallSize.Large)]
        [InlineData(ProductCallSize.Medium)]
        [InlineData(ProductCallSize.Max)]
        [InlineData(ProductCallSize.Small)]
        public void CallSizeDefined_ReturnsCorrectAmountOfProducs(ProductCallSize callSize)
        {
            var response = SteamApiClient.GetSteamProductsAsync(IncludeProducts.Games, callSize: callSize)
                .Result;
            SleepAfterSendingRequest();

            Assert.True((int)callSize >= response.Count);
        }

        /// <summary>
        /// Test case for IncludeProducts enum used as a set. Method should return
        /// both DLCs and Games
        /// </summary>
        [Fact]
        public void ProductTypeUsedAsSet_ReturnsRequestedSteamProducts()
        {
            var response1 = SteamApiClient.GetSteamProductsAsync(IncludeProducts.GameProducs)
                .Result;
            SleepAfterSendingRequest();
            var response2 = SteamApiClient.GetSteamProductsAsync(IncludeProducts.DLC | IncludeProducts.Games)
                .Result;
            SleepAfterSendingRequest();

            for (int i = 0; i < response1.Count; i++)
            {
                Assert.True(response1[i].AppId == response2[i].AppId);
            }

        }

        /// <summary>
        /// Test case for all multiple product types included in the reqeust.
        /// </summary>
        /// <param name="products">product type</param>
        [Theory]
        [InlineData(IncludeProducts.All)]
        [InlineData(IncludeProducts.GameProducs)]
        [InlineData(IncludeProducts.Media)]
        public void MultipleProductTypes_ReturnsRequestedSteamProducts(IncludeProducts products)
        {
            var response = SteamApiClient.GetSteamProductsAsync(products)
                .Result;
            SleepAfterSendingRequest();

            Assert.NotEmpty(response);
            Assert.All(response, product => {
                Assert.True(product.AppId != 0);
                Assert.True(product.LastModified != 0);
            });
        }


        /// <summary>
        /// Test case for all single product types included in the reqeust.
        /// </summary>
        /// <param name="products">product type</param>
        [Theory]
        [InlineData(IncludeProducts.DLC)]
        [InlineData(IncludeProducts.Games)]
        [InlineData(IncludeProducts.Harware)]
        [InlineData(IncludeProducts.Software)]
        [InlineData(IncludeProducts.Videos)]
        public void SingleProductType_ReturnsRequestedSteamProducts(IncludeProducts products)
        {
            var response = SteamApiClient.GetSteamProductsAsync(products)
                .Result;
            SleepAfterSendingRequest();

            Assert.NotEmpty(response);
            Assert.All(response, product => {
                Assert.True(product.AppId != 0);
                Assert.True(product.LastModified != 0);
            });
        }
    }
}
