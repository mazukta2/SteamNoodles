using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Tests.Environment.Game;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Basic
{
    public class LocalizationTests
    {
        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }

        [Test]
        public void LocalizationManagerChangesLanguage()
        {
            var list = new List<LanguagePack>()
            {
                new LanguagePack("en", new Dictionary<string, string>() { {"test1", "testresult1" } }),
                new LanguagePack("ja", new Dictionary<string, string>() { {"test1", "testresult2" } })
            };

            var localizationManager = new LocalizationManager(list, "en");

            var localizationString = new LocalizatedString(localizationManager, "test1");

            Assert.AreEqual("testresult1", localizationString.Get());

            localizationManager.ChangeLanguage("ja");

            Assert.AreEqual("testresult2", localizationString.Get());
        }
    }
}
