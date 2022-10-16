using Game.Assets.Scripts.Tests.Definitions;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Views.Level.Units;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Customers
{
    public class CrowdTests
    {
        [Test]
        public void IsCrowdUnitsSpawned()
        {
            var game = new BuildConstructor()
                .UpdateDefinition<LevelDefinitionMock>((d) => d.LevelSettings.CrowdUnitsAmount = 15)
                .Build();

            Assert.AreEqual(1, game.Views.FindViews<UnitsManagerView>().Count);
            Assert.AreEqual(15, game.Views.FindViews<UnitView>().Count);

            game.Dispose();
        }

        [Test]
        public void CrowdIsMoving()
        {
            var game = new BuildConstructor()
                .UpdateDefinition<LevelDefinitionMock>((d) => d.LevelSettings.CrowdUnitsAmount = 1)
                .UpdateDefinition<LevelDefinitionMock>((d) => d.LevelSettings.UnitsRect = new Scripts.Game.Logic.Common.Math.FloatRect(-10, -10, 10, 10))
                .Build();

            var unit = game.Views.FindView<UnitView>();
            var startingPosition = unit.Position.Value.X;
            Assert.IsFalse(unit.IsDisposed);
            game.Time.MoveTime(100);
            Assert.AreNotEqual(startingPosition, unit.Position.Value.X);
            Assert.IsTrue(unit.IsDisposed);

            game.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
