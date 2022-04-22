using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment.Views;
using Game.Assets.Scripts.Tests.Managers.Game;
using Game.Assets.Scripts.Tests.Mocks.Levels;
using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Basic
{
    public class LocalizationTests
    {
        [Test]
        public void IsLocalizationStringWorking()
        {
            var build = new GameTestConstructor().Build();
          


            build.Dispose();
        }


        [Test]
        public void IsDynamicChangesWorking()
        {
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
