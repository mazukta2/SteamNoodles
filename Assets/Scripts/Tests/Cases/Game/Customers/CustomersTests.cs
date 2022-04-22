using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Views.Level.Units;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;
using Game.Tests.Cases;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Game.Customers
{
    public class CustomersTests
    {
        #region Queue

        [Test]
        public void IsPointsCalculationsCorrect()
        {
            var points = new BuildingPoints(2, 2);
            TestLevel(0, 0);
            TestLevel(1, 0);
            TestLevel(2, 0);
            TestLevel(3, 1);
            TestLevel(4, 1);
            TestLevel(5, 1);
            TestLevel(6, 1);
            TestLevel(7, 1);
            TestLevel(8, 2);
            TestLevel(9, 2);

            void TestLevel(int currentPoints, int level)
            {
                points.Value = currentPoints;
                Assert.AreEqual(level, points.CurrentLevel);
            }
        }

        [Test]
        public void IsPointsConvertsToQueueSize()
        {
            var game = new GameConstructor()
                .UpdateDefinition<ConstructionsSettingsDefinition>(c => c.CellSize = 1)
                .UpdateDefinition<ConstructionDefinition>(x => x.Points = 9)
                .UpdateDefinition<LevelDefinitionMock>((d) => d.CrowdUnitsAmount = 0)
                .Build();

            Assert.AreEqual("0/3", game.CurrentLevel.FindView<MainScreenView>().Points.Value);
            Assert.AreEqual(0, game.CurrentLevel.FindView<MainScreenView>().PointsProgress.Value);
            Assert.AreEqual(0, game.CurrentLevel.FindViews<UnitView>().Count);

            game.CurrentLevel.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            var points = new BuildingPoints(2, 2);
            points.Value = 9;
            Assert.AreEqual(2, points.CurrentLevel);
            Assert.AreEqual(15, points.PointsForNextLevel);
            Assert.IsTrue(points.Progress != 0);

            Assert.AreEqual("9/15", game.CurrentLevel.FindView<MainScreenView>().Points.Value);
            Assert.AreEqual(points.Progress, game.CurrentLevel.FindView<MainScreenView>().PointsProgress.Value);
            Assert.AreEqual(1, game.CurrentLevel.FindViews<UnitView>().Count);

            game.Dispose();
        }

        #endregion

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
