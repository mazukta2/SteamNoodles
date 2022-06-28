using Game.Assets.Scripts.Tests.Environment.Game;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Json.Convertors;

namespace Game.Assets.Scripts.Tests.Cases.Definitions
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
            var commmonField = new CommonCase();
            var build = new GameConstructor()
                .AddDefinition("testDef", commmonField)
                .Build();

            var str = @"{ 
                ""Test"" : ""testDef""
            }";

            var item = JsonConvert.DeserializeObject<DefinitionsCase>(str);
            Assert.AreEqual(commmonField, item.Test);

            build.Dispose();
        }

        [Test]
        public void DictionarySettingsConversion()
        {
            var commmonField = new CommonCase();
            var build = new GameConstructor()
                .AddDefinition("testDef", commmonField)
                .Build();

            var str = @"{ 
                ""Test"" : {
                    ""testDef"" : 4
                }
            }";
            var item = JsonConvert.DeserializeObject<DefinitionDictionaryCase>(str);
            Assert.AreEqual(1, item.Test.Count);
            Assert.IsTrue(item.Test.ContainsKey(commmonField));
            Assert.AreEqual(4, item.Test[commmonField]);

            build.Dispose();
        }

        //[Test]
        //public void AssetsConversion()
        //{
        //    var game = new GameController();
        //    var testLevel = new ItsUnitySpriteWrapper();
        //    game.Assets.Add("testsprie", testLevel);
        //    var str = @"{ 
        //        ""Test"" : ""testsprie""
        //    }";
        //    var item = JsonConvert.DeserializeObject<AssetsCase>(str);
        //    Assert.AreEqual(testLevel, item.Test);
        //    game.Dispose();
        //}


        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }

        public class CommonCase
        {
            public string Test { get; set; }
        }

        public class DefinitionsCase
        {
            [JsonConverter(typeof(DefinitionsConventer<CommonCase>))]
            public CommonCase Test { get; set; }
        }

        public class DefinitionDictionaryCase
        {
            [JsonConverter(typeof(DefinitionsDictionaryConventer<CommonCase, int>))]
            public IReadOnlyDictionary<CommonCase, int> Test { get; set; }
        }

        //public class AssetsCase
        //{
        //    [JsonConverter(typeof(AssetConventer))]
        //    public ISprite Test { get; set; }
        //}
    }
}
