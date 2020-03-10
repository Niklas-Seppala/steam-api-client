using SteamApi.Models.Steam;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Xunit;

namespace Models
{
    /// <summary>
    /// Test class for Steam Api model serialization
    /// </summary>
    public class SteamApiModelSerialization_Tests
    {
        /// <summary>
        /// Serializes model and the creates new model to deserialize
        /// the data.
        /// </summary>
        /// <typeparam name="T">Type of the model</typeparam>
        /// <param name="model">Steam api model</param>
        /// <returns>deserialized model</returns>
        private static T SerializeAndDeserialize<T>(T model)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, model);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }


        /// <summary>
        /// Testing binary serialization for Friend model
        /// </summary>
        [Fact]
        public void FriendModel_BinarySerialization_Serializes()
        {
            Friend friend = new Friend()
            {
                FriendSince = 1520721662,
                Id64 = 76561198107435620
            };
            var deserialized = SerializeAndDeserialize(friend);
            Assert.Equal(friend.Id64, deserialized.Id64);
            Assert.Equal(friend.FriendSince, deserialized.FriendSince);
        }


        /// <summary>
        /// Testing binary serialization for AccountsBans model
        /// </summary>
        [Fact]
        public void AccountBansModel_BinarySerialization_Serializes()
        {
            AccountBans bans = new AccountBans()
            {
                Id64 = 76561198107435620,
                CommunityBanned = false,
                DaysSinceLastBan = 0,
                EconomyBan = "none",
                VACBanned = false,
                NumberOfVACBans = 0,
                NumberOfGameBans = 0
            };
            AccountBans deserialized = SerializeAndDeserialize(bans);

            Assert.Equal(bans.Id64, deserialized.Id64);
            Assert.Equal(bans.NumberOfGameBans, deserialized.NumberOfGameBans);
            Assert.Equal(bans.DaysSinceLastBan, deserialized.DaysSinceLastBan);
            Assert.Equal(bans.EconomyBan, deserialized.EconomyBan);
        }


        /// <summary>
        /// Testing binary serialization for SteamAccount model
        /// </summary>
        [Fact]
        public void SteamAccountModel_BinarySerialization_Serializes()
        {
            SteamAccount account = new SteamAccount()
            {
                Id64 = 76561198107435620,
                Game = "Dota 2",
                TimeCreated = 1520721662,
                PersonaName = "Player"
            };

            var deserialized = SerializeAndDeserialize(account);
            Assert.Equal(account.PersonaName, deserialized.PersonaName);
            Assert.Equal(account.Id64, deserialized.Id64);
            Assert.Equal(account.Game, deserialized.Game);
            Assert.Equal(account.TimeCreated, deserialized.TimeCreated);
        }


        /// <summary>
        /// Testing binary serialization for AppNewsCollection model
        /// </summary>
        [Fact]
        public void AppNewsCollection_BinarySerialization_Serializes()
        {
            AppNewsCollection appNews = new AppNewsCollection()
            {
                AppId = 570,
                NewsItems = new List<AppNews>()
                {
                    new AppNews()
                    {
                        AppId = 570,
                        Author = "Niklas",
                        Date = 1520721662,
                        Title = "jee"
                    },
                    new AppNews()
                    {
                        AppId = 570,
                        Author = "Jenni",
                        Date = 1520721662,
                        Title = "juu"
                    }
                },
                TotalCount = 200,
            };

            var deserialized = SerializeAndDeserialize(appNews);
            Assert.Equal(appNews.AppId, deserialized.AppId);
            Assert.Equal(appNews.TotalCount, deserialized.TotalCount);
            for (int i = 0; i < appNews.NewsItems.Count; i++)
            {
                Assert.Equal(appNews.NewsItems[i].AppId, deserialized.NewsItems[i].AppId);
                Assert.Equal(appNews.NewsItems[i].Author, deserialized.NewsItems[i].Author);
                Assert.Equal(appNews.NewsItems[i].Date, deserialized.NewsItems[i].Date);
                Assert.Equal(appNews.NewsItems[i].Title, deserialized.NewsItems[i].Title);
            }
        }


        /// <summary>
        /// Testing binary searialization for SteamProduct model
        /// </summary>
        [Fact]
        public void SteamProductModel_BinarySerialization_Serializes()
        {
            var product = new SteamProduct()
            {
                AppId = 570,
                LastModified = 1520721662,
                Name = "dota2",
                PriceChangeNumber = 1326721652
            };

            var deserialized = SerializeAndDeserialize(product);

            Assert.Equal(product.AppId, deserialized.AppId);
            Assert.Equal(product.LastModified, deserialized.LastModified);
            Assert.Equal(product.Name, deserialized.Name);
            Assert.Equal(product.PriceChangeNumber, deserialized.PriceChangeNumber);
        }


        /// <summary>
        /// Testing binary serialization for ApiInterface model
        /// </summary>
        [Fact]
        public void ApiInterfaceModel_BinarySerialization_Serializes()
        {
            var apiInterface = new ApiInterface()
            {
                Methods = new List<Method>()
                {
                    new Method()
                    {
                        Description = "get things done",
                        HttpMethod = "GET",
                        Name = "GetThings",
                        Parameters = new List<Parameter>()
                        {
                            new Parameter()
                            {
                                Name = "key",
                                Description = "dev key",
                                Optional = false,
                                Type = ""
                            },
                        }
                    },
                    new Method()
                    {
                        Description = "get other things done",
                        HttpMethod = "GET",
                        Name = "GetOtherThings",
                        Parameters = new List<Parameter>()
                        {
                            new Parameter()
                            {
                                Name = "key",
                                Description = "dev key",
                                Optional = false,
                                Type = ""
                            },
                        }
                    }
                },
                Name = "IRandom"
            };

            var deserialized = SerializeAndDeserialize(apiInterface);
            Assert.Equal(apiInterface.Name, deserialized.Name);
            Assert.Equal(apiInterface.Methods[0].Name, deserialized.Methods[0].Name);
            Assert.Equal(apiInterface.Methods[0].Description, deserialized.Methods[0].Description);
            Assert.Equal(apiInterface.Methods[0].HttpMethod, deserialized.Methods[0].HttpMethod);
        }


        /// <summary>
        /// Testing binary serialization for SteamServerInfo model
        /// </summary>
        [Fact]
        public void SteamServerInfoModel_BinarySerialization_Serializes()
        {
            var serverInfo = new SteamServerInfo()
            {
                ServerLocalTime = DateTime.Now.ToString(),
                ServerTime = 1520721662
            };

            var deserialized = SerializeAndDeserialize(serverInfo);
            Assert.Equal(serverInfo.ServerLocalTime, deserialized.ServerLocalTime);
            Assert.Equal(serverInfo.ServerTime, deserialized.ServerTime);
        }
    }
}
