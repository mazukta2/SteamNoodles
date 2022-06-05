using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Customers
{
    public class CrowdTests
    {
        [Test]
        public void IsCrowdUnitsSpawned()
        {
            throw new System.Exception();
            //var game = new GameConstructor()
            //    .UpdateDefinition<LevelDefinitionMock>((d) => d.CrowdUnitsAmount = 15)
            //    .Build();

            //Assert.AreEqual(1, game.LevelCollection.FindViews<UnitsManagerView>().Count);
            //Assert.AreEqual(15, game.LevelCollection.FindViews<UnitView>().Count);

            //game.Dispose();
        }

        [Test]
        public void CrowdIsMoving()
        {
            throw new System.Exception();
            //var game = new GameConstructor()
            //    .UpdateDefinition<LevelDefinitionMock>((d) => d.CrowdUnitsAmount = 1)
            //    .UpdateDefinition<LevelDefinitionMock>((d) => d.UnitsRect = new Scripts.Game.Logic.Common.Math.FloatRect(-10, -10, 10, 10))
            //    .Build();

            //var unit = game.LevelCollection.FindView<UnitView>();
            //var startingPosition = unit.Position.Value.X;
            //Assert.IsFalse(unit.IsDisposed);
            //game.Time.MoveTime(100);
            //Assert.AreNotEqual(startingPosition, unit.Position.Value.X);
            //Assert.IsTrue(unit.IsDisposed);

            //game.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
