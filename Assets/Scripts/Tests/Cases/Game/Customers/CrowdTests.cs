using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Tests.Managers.Game;
using Game.Tests.Cases;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Customers
{
    public class CrowdTests
    {
        [Test]
        public void IsCrowdUnitsSpawned()
        {
            var game = new GameTestConstructor()
                .UpdateDefinition<LevelDefinitionMock>((d) => d.CrowdUnitsAmount = 15)
                .Build();

            Assert.AreEqual(15, game.CurrentLevel.FindViews<UnitView>());

            game.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
