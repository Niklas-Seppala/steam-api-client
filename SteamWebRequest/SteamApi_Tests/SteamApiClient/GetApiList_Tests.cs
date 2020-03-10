using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Client
{
    public class GetApiList_Tests : SteamApiClientTests
    {
        public GetApiList_Tests(ClientFixture fixture) : base(fixture) { }


        [Fact]
        public void DefaultConditions_ReturnsApiList()
        {
            var response = SteamApiClient.GetApiListAsync()
                .Result;
            SleepAfterSendingRequest();

            Assert.NotEmpty(response);
            Assert.All(response, resp => {

                Assert.NotNull(resp.Methods);
                Assert.NotEmpty(resp.Methods);
                Assert.NotEmpty(resp.Name);
                Assert.All(resp.Methods, method => {
                    Assert.NotEmpty(method.Name);
                    Assert.True(method.Version != 0);
                    Assert.NotEmpty(method.HttpMethod);
                });
            });
        }
    }
}
