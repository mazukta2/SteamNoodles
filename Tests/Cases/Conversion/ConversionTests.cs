using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Settings.Rewards;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;
using Game.Tests.Mocks.Settings.Rewards;
using Game.Tests.Mocks.Views.Common;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace Game.Tests.Cases.Settings
{
    public class ConversionTests
    {
        [Test]
        public void CommonConversion()
        {
            var item = JsonConvert.DeserializeObject<CommonCase>(@"{ ""Test"" : ""passed""}");
            Assert.AreEqual("passed", item.Test);
        }

        [Test]
        public void SettingsConversion()
        {
            var game = new GameController();
            var testLevel = new LevelSettings();
            game.Settings.Add("testlevel", testLevel);
            var str = @"{ 
                ""Test"" : ""testlevel""
            }";
            var item = JsonConvert.DeserializeObject<LevelCase>(str);
            Assert.AreEqual(testLevel, item.Test);
            game.Dispose();
        }

        [Test]
        public void DictionarySettingsConversion()
        {
            var game = new GameController();
            var testLevel = new LevelSettings();
            game.Settings.Add("testlevel", testLevel);
            var str = @"{ 
                ""Test"" : {
                    ""testlevel"" : 4
                }
            }";
            var item = JsonConvert.DeserializeObject<LevelDictionaryCase>(str);
            Assert.AreEqual(1, item.Test.Count);
            Assert.IsTrue(item.Test.ContainsKey(testLevel));
            Assert.AreEqual(4, item.Test[testLevel]);
            game.Dispose();
        }


        [Test]
        public void Objectonversion()
        {
            var str = @"{ 
                ""Test"" : {
                    ""MinToHand"" : 1,
                    ""MaxTohand"" : 3
                }
            }";
            var item = JsonConvert.DeserializeObject<RewardCase>(str);
            Assert.AreEqual(1, item.Test.MinToHand);
            Assert.AreEqual(3, item.Test.MaxToHand);
        }


        [Test]
        public void AssetsConversion()
        {
            var game = new GameController();
            var testLevel = new ItsUnitySpriteWrapper();
            game.Assets.Add("testsprie", testLevel);
            var str = @"{ 
                ""Test"" : ""testsprie""
            }";
            var item = JsonConvert.DeserializeObject<AssetsCase>(str);
            Assert.AreEqual(testLevel, item.Test);
            game.Dispose();
        }

        [Test]
        public void MathConversion()
        {
            var item = JsonConvert.DeserializeObject<MathCase>(@"{ ""Point"" : [1,2], ""Rect"":[3,4,5,6]}");
            Assert.AreEqual(1, item.Point.X);
            Assert.AreEqual(2, item.Point.Y);
            Assert.AreEqual(3, item.Rect.X);
            Assert.AreEqual(4, item.Rect.Y);
            Assert.AreEqual(5, item.Rect.Width);
            Assert.AreEqual(6, item.Rect.Height);
        }


        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }

        public class CommonCase
        {
            public string Test { get; set; }
        }

        public class LevelCase
        {
            [JsonConverter(typeof(SettingsConventer<LevelSettings>))]
            public ILevelSettings Test { get; set; }
        }

        public class LevelDictionaryCase
        {
            [JsonConverter(typeof(SettingsDictionaryConventer<ILevelSettings, LevelSettings, int>))]
            public IReadOnlyDictionary<ILevelSettings, int> Test { get; set; }
        }

        public class RewardCase
        {
            [JsonConverter(typeof(ObjectConventer<Reward>))]
            public IReward Test { get; set; }
        }

        public class MathCase
        {
            [JsonConverter(typeof(PointConventer))]
            public Point Point { get; set; }
            [JsonConverter(typeof(RectConventer))]
            public Rect Rect { get; set; }
        }

        public class AssetsCase
        {
            [JsonConverter(typeof(AssetConventer))]
            public ISprite Test { get; set; }
        }

    }
}
