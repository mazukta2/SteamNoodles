using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Common.Creation;
using Game.Assets.Scripts.Tests.Views.Ui.Screens.Widgets;
using Game.Tests.Controllers;
using NUnit.Framework;

namespace Game.Tests.Cases.Customers
{
    public class ResourcesTests
    {
        [Test]
        public void IsBuildingPointChangeLevel()
        {
            var buildingPoints = new BuildingPoints(2, 2);
            var levelUps = 0;
            var levelDowns = 0;
            buildingPoints.OnLevelUp += BuildingPoints_OnLevelUp;
            buildingPoints.OnLevelDown += BuildingPoints_OnLevelDown;

            Assert.AreEqual(0, buildingPoints.CurrentLevel);
            Assert.AreEqual(0, buildingPoints.PointsForCurrentLevel);
            Assert.AreEqual(3, buildingPoints.PointsForNextLevel);
            Assert.AreEqual(0, levelUps);
            Assert.AreEqual(0, levelDowns);

            buildingPoints.Value += 2;

            Assert.AreEqual(0, buildingPoints.CurrentLevel);
            Assert.AreEqual(0, buildingPoints.PointsForCurrentLevel);
            Assert.AreEqual(3, buildingPoints.PointsForNextLevel);
            Assert.AreEqual(0, levelUps);
            Assert.AreEqual(0, levelDowns);

            buildingPoints.Value += 1;

            Assert.AreEqual(1, buildingPoints.CurrentLevel);
            Assert.AreEqual(3, buildingPoints.PointsForCurrentLevel);
            Assert.AreEqual(8, buildingPoints.PointsForNextLevel);
            Assert.AreEqual(1, levelUps);
            Assert.AreEqual(0, levelDowns);

            buildingPoints.Value -= 1;

            Assert.AreEqual(0, buildingPoints.CurrentLevel);
            Assert.AreEqual(0, buildingPoints.PointsForCurrentLevel);
            Assert.AreEqual(3, buildingPoints.PointsForNextLevel);
            Assert.AreEqual(1, levelUps);
            Assert.AreEqual(1, levelDowns);

            buildingPoints.OnLevelUp -= BuildingPoints_OnLevelUp;
            buildingPoints.OnLevelDown -= BuildingPoints_OnLevelDown;

            void BuildingPoints_OnLevelUp()
            {
                levelUps++;
            }

            void BuildingPoints_OnLevelDown()
            {
                levelDowns++;
            }
        }

        [Test]
        public void IsBuildingPointAdditionalPointsWorks()
        {
            var buildingPoints = new BuildingPoints(2, 2);
            buildingPoints.Value += 3;

            Assert.AreEqual(8, buildingPoints.PointsForNextLevel);
            // 8 - 3 = 5;
            Assert.AreEqual(0.2f, buildingPoints.GetChangedValue(1).Progress);
            Assert.AreEqual(2/3f, buildingPoints.GetChangedValue(-1).Progress);
        }


        [Test]
        public void IsCoinsViewWorking()
        {
            var coins = new Coins();
            var collection = new ViewsCollection();

            var view = new CustumerCoinsMock(collection);
            new CustumerCoinsPresenter(coins, view);

            Assert.AreEqual(2, view.Text.Value);
            coins.Change(2);
            Assert.AreEqual(2, view.Text.Value);
            coins.Change(2);
            Assert.AreEqual(4, view.Text.Value);
            coins.Change(-10);
            Assert.AreEqual(0, view.Text.Value);

            collection.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
