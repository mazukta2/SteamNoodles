using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Models.Services;
using Game.Assets.Scripts.Game.Logic.Models.Services.Definitions;
using Game.Tests.Cases;
using Game.Tests.Controllers;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
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
            CreateDefinitions(new DefinitionsMock()
                .Add("test", commmonField));

            var str = @"{ 
                ""Test"" : ""test""
            }";

            var item = JsonConvert.DeserializeObject<DefinitionsCase>(str);
            Assert.AreEqual(commmonField, item.Test);
            DestoryDefinitions();
        }

        [Test, Order(TestCore.EssentialOrder)]
        public void DictionarySettingsConversion()
        {
            var commmonField = new CommonCase();
            CreateDefinitions(new DefinitionsMock()
                .Add("test", commmonField));

            var str = @"{ 
                ""Test"" : {
                    ""test"" : 4
                }
            }";
            var item = JsonConvert.DeserializeObject<DefinitionDictionaryCase>(str);
            Assert.AreEqual(1, item.Test.Count);
            Assert.IsTrue(item.Test.ContainsKey(commmonField));
            Assert.AreEqual(4, item.Test[commmonField]);
            DestoryDefinitions();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }

        private void CreateDefinitions(DefinitionsMock mock)
        {
            var services = new ServiceManager();
            IModelServices.Default = services;

            var definitionService = new DefinitionsService(services, mock, false);
            services.Add(definitionService);

        }

        private void DestoryDefinitions()
        {
            ((IDisposable)IModelServices.Default).Dispose();
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
