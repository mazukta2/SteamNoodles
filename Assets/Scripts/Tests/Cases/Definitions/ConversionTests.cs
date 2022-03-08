using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace Game.Tests.Cases.Settings
{
    public class ConversionTests
    {
        //[Test]
        //public void CommonConversion()
        //{
        //    var item = JsonConvert.DeserializeObject<CommonCase>(@"{ ""Test"" : ""passed""}");
        //    Assert.AreEqual("passed", item.Test);
        //} 

        //[Test]
        //public void SettingsConversion()
        //{
        //    var game = new GameController();
        //    var testLevel = new LevelDefinition();
        //    game.Settings.Add("testlevel", testLevel);
        //    var str = @"{ 
        //        ""Test"" : ""testlevel""
        //    }";
        //    var item = JsonConvert.DeserializeObject<LevelCase>(str);
        //    Assert.AreEqual(testLevel, item.Test);
        //    game.Dispose();
        //}

        //[Test]
        //public void DictionarySettingsConversion()
        //{
        //    var game = new GameController();
        //    var testLevel = new LevelDefinition();
        //    game.Settings.Add("testlevel", testLevel);
        //    var str = @"{ 
        //        ""Test"" : {
        //            ""testlevel"" : 4
        //        }
        //    }";
        //    var item = JsonConvert.DeserializeObject<LevelDictionaryCase>(str);
        //    Assert.AreEqual(1, item.Test.Count);
        //    Assert.IsTrue(item.Test.ContainsKey(testLevel));
        //    Assert.AreEqual(4, item.Test[testLevel]);
        //    game.Dispose();
        //}

        ////[Test]
        ////public void AssetsConversion()
        ////{
        ////    var game = new GameController();
        ////    var testLevel = new ItsUnitySpriteWrapper();
        ////    game.Assets.Add("testsprie", testLevel);
        ////    var str = @"{ 
        ////        ""Test"" : ""testsprie""
        ////    }";
        ////    var item = JsonConvert.DeserializeObject<AssetsCase>(str);
        ////    Assert.AreEqual(testLevel, item.Test);
        ////    game.Dispose();
        ////}


        //[TearDown]
        //public void TestDisposables()
        //{
        //    DisposeTests.TestDisposables();
        //}

        //public class CommonCase
        //{
        //    public string Test { get; set; }
        //}

        //public class LevelCase
        //{
        //    [JsonConverter(typeof(DefinitionsConventer<LevelDefinition>))]
        //    public ILevelDefinition Test { get; set; }
        //}

        //public class LevelDictionaryCase
        //{
        //    [JsonConverter(typeof(SettingsDictionaryConventer<ILevelDefinition, LevelDefinition, int>))]
        //    public IReadOnlyDictionary<ILevelDefinition, int> Test { get; set; }
        //}



        //public class AssetsCase
        //{
        //    [JsonConverter(typeof(AssetConventer))]
        //    public ISprite Test { get; set; }
        //}

    }
}
