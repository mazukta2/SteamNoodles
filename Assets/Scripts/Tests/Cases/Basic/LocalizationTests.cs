using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Basic
{
    public class LocalizationTests
    {
        [Test]
        public void IsLocalizationStringWorking()
        {
            var build = new GameConstructor().Build();
          


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
