using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Tests.Cases;
using Game.Tests.Controllers;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Cases.Definitions
{
    public class JsonConversionTests
    {
        [Test, Order(TestCore.EssentialOrder)]
        public void CommonConversion()
        {
            var item = JsonConvert.DeserializeObject<CommonCase>(@"{ ""Test"" : ""passed""}");
            Assert.AreEqual("passed", item.Test);
        }

        [Test, Order(TestCore.EssentialOrder)]
        public void SettingsConversion()
        {
            var commmonField = new CommonCase();
            IGameDefinitions.Default = new DefinitionsMock()
                .Add("test", commmonField);

            var str = @"{ 
                ""Test"" : ""test""
            }";

            var item = JsonConvert.DeserializeObject<DefinitionsCase>(str);
            Assert.AreEqual(commmonField, item.Test);
        }

        [Test, Order(TestCore.EssentialOrder)]
        public void DictionarySettingsConversion()
        {
            var commmonField = new CommonCase();
            IGameDefinitions.Default = new DefinitionsMock()
                .Add("test", commmonField);

            var str = @"{ 
                ""Test"" : {
                    ""test"" : 4
                }
            }";
            var item = JsonConvert.DeserializeObject<DefinitionDictionaryCase>(str);
            Assert.AreEqual(1, item.Test.Count);
            Assert.IsTrue(item.Test.ContainsKey(commmonField));
            Assert.AreEqual(4, item.Test[commmonField]);
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
    }
}
