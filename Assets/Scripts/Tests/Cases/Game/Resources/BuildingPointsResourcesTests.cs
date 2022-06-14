using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using Game.Assets.Scripts.Tests.Views.Ui.Screens.Widgets;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Resources
{
    public class BuildingPointsResourcesTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void IsPointsCalculationsCorrect()
        {
            var points = new BuildingPointsCalculator(2, 2);
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
            TestLevel(20, 3);

            void TestLevel(int currentPoints, int level)
            {
                points.Value = currentPoints;
                Assert.AreEqual(level, points.CurrentLevel);
            }
        }


        [Test, Order(TestCore.PresenterOrder)]
        public void IsPointsPresenting()
        {
            var time = new GameTime();
            var pointsService = new BuildingPointsService(0, 0, time, 2, 2);

            var viewCollection = new ViewsCollection();
            var view = new PointCounterWidgetView(viewCollection);
            new PointCounterWidgetPresenter(view,
                new ProgressBarSliders(view.PointsProgress, time, 0, 0),
                pointsService);

            Assert.AreEqual("0/3", view.Points.Value);

            pointsService.Change(new BuildingPoints(5));

            Assert.AreEqual("5/8", view.Points.Value);

            viewCollection.Dispose();
            pointsService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsPointsProgressPresenting()
        {
            var time = new GameTime();
            var points = new BuildingPointsService(0, 0, time, 2.2f, 8);

            var levelCollection = new ViewsCollection();
            var view = new PointCounterWidgetView(levelCollection);
            var spawner = new PieceSpawnerView(levelCollection);
            new PointPieceSpawnerPresenter(spawner);
            new PointCounterWidgetPresenter(view, new ProgressBarSliders(view.PointsProgress, time, 0, 0), points);

            points.Change(new BuildingPoints(1));
            Assert.AreEqual(1 / 9f, view.PointsProgress.MainValue);
            points.Change(new BuildingPoints(8));
            Assert.AreEqual(0, view.PointsProgress.MainValue);
            points.Change(new BuildingPoints(1));

            Assert.AreEqual(20, points.GetPointsForNextLevel());
            Assert.AreEqual(10, points.GetValue());
            Assert.AreEqual(9, points.GetPointsForCurrentLevel());
            Assert.AreEqual(1 / 11f, points.GetProgress());

            Assert.AreEqual(1 / 11f, view.PointsProgress.MainValue);
            points.Change(new BuildingPoints(10));
            Assert.AreEqual(20, points.GetValue());
            Assert.AreEqual(20, points.GetPointsForCurrentLevel());
            Assert.AreEqual(0, points.GetProgress());
            Assert.AreEqual(0, view.PointsProgress.MainValue);

            levelCollection.Dispose();
            points.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsBuildingPointChangeLevel()
        {
            var time = new GameTime();
            var buildingPoints = new BuildingPointsService(0, 0, time, 2, 2);
            var levelUps = 0;
            var levelDowns = 0;
            buildingPoints.OnCurrentLevelUp += BuildingPoints_OnLevelUp;
            buildingPoints.OnCurrentLevelDown += BuildingPoints_OnLevelDown;

            Assert.AreEqual(new BuildingLevel(0), buildingPoints.GetCurrentLevel());
            Assert.AreEqual(0, buildingPoints.GetPointsForCurrentLevel());
            Assert.AreEqual(3, buildingPoints.GetPointsForNextLevel());
            Assert.AreEqual(0, levelUps);
            Assert.AreEqual(0, levelDowns);

            buildingPoints.Change(new BuildingPoints(2));

            Assert.AreEqual(new BuildingLevel(0), buildingPoints.GetCurrentLevel());
            Assert.AreEqual(0, buildingPoints.GetPointsForCurrentLevel());
            Assert.AreEqual(3, buildingPoints.GetPointsForNextLevel());
            Assert.AreEqual(0, levelUps);
            Assert.AreEqual(0, levelDowns);

            buildingPoints.Change(new BuildingPoints(1));

            Assert.AreEqual(new BuildingLevel(1), buildingPoints.GetCurrentLevel());
            Assert.AreEqual(3, buildingPoints.GetPointsForCurrentLevel());
            Assert.AreEqual(8, buildingPoints.GetPointsForNextLevel());
            Assert.AreEqual(1, levelUps);
            Assert.AreEqual(0, levelDowns);

            buildingPoints.Change(new BuildingPoints(-1));

            Assert.AreEqual(new BuildingLevel(0), buildingPoints.GetCurrentLevel());
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

        [Test, Order(TestCore.ModelOrder)]
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

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
