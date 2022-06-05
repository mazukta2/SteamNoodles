using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Ui.Screens.Widgets;
using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Resources
{
    public class ResourcesTests
    {
        [Test]
        public void IsBuildingPointChangeLevel()
        {
            var time = new GameTime();
            var buildingPoints = new BuildingPointsService(0, 0, time, 2, 2);
            var levelUps = 0;
            var levelDowns = 0;
            buildingPoints.OnCurrentLevelUp += BuildingPoints_OnLevelUp;
            buildingPoints.OnCurrentLevelDown += BuildingPoints_OnLevelDown;

            Assert.AreEqual(0, buildingPoints.GetCurrentLevel());
            Assert.AreEqual(0, buildingPoints.GetPointsForCurrentLevel());
            Assert.AreEqual(3, buildingPoints.GetPointsForNextLevel());
            Assert.AreEqual(0, levelUps);
            Assert.AreEqual(0, levelDowns);

            buildingPoints.Change(new BuildingPoints(2));

            Assert.AreEqual(0, buildingPoints.GetCurrentLevel());
            Assert.AreEqual(0, buildingPoints.GetPointsForCurrentLevel());
            Assert.AreEqual(3, buildingPoints.GetPointsForNextLevel());
            Assert.AreEqual(0, levelUps);
            Assert.AreEqual(0, levelDowns);

            buildingPoints.Change(new BuildingPoints(1));

            Assert.AreEqual(1, buildingPoints.GetCurrentLevel());
            Assert.AreEqual(3, buildingPoints.GetPointsForCurrentLevel());
            Assert.AreEqual(8, buildingPoints.GetPointsForNextLevel());
            Assert.AreEqual(1, levelUps);
            Assert.AreEqual(0, levelDowns);

            buildingPoints.Change(new BuildingPoints(-1));

            Assert.AreEqual(0, buildingPoints.GetCurrentLevel());
            Assert.AreEqual(0, buildingPoints.GetPointsForCurrentLevel());
            Assert.AreEqual(3, buildingPoints.GetPointsForNextLevel());
            Assert.AreEqual(1, levelUps);
            Assert.AreEqual(1, levelDowns);

            buildingPoints.OnCurrentLevelUp -= BuildingPoints_OnLevelUp;
            buildingPoints.OnCurrentLevelDown -= BuildingPoints_OnLevelDown;

            buildingPoints.Dispose();

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
            var buildingPoints = new BuildingPointsCalculator(2, 2);
            buildingPoints.Value += 3;

            Assert.AreEqual(8, buildingPoints.PointsForNextLevel);
            // 8 - 3 = 5;
            buildingPoints.Value += 1;
            Assert.AreEqual(0.2f, buildingPoints.Progress);
            buildingPoints.Value += -2;
            Assert.AreEqual(2 / 3f, buildingPoints.Progress);
        }


        [Test]
        public void IsCoinsViewWorking()
        {
            var coins = new CoinsService();
            var collection = new ViewsCollection();

            var view = new CustumerCoinsMock(collection);
            new CustumerCoinsPresenter(coins, view);

            Assert.AreEqual("0", view.Text.Value);
            coins.Change(2);
            Assert.AreEqual("2", view.Text.Value);
            coins.Change(2);
            Assert.AreEqual("4", view.Text.Value);
            coins.Change(-10);
            Assert.AreEqual("0", view.Text.Value);

            collection.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
