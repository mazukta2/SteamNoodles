using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using Game.Assets.Scripts.Tests.Views.Level.Units;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;
using Game.Tests.Cases;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Collections.Generic;
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

            Assert.AreEqual("0/3", game.LevelCollection.FindView<PointCounterWidgetView>().Points.Value);
            Assert.AreEqual(0, game.LevelCollection.FindView<PointCounterWidgetView>().PointsProgress.MainValue);
            Assert.AreEqual(0, game.LevelCollection.FindViews<UnitView>().Count);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            var points = new BuildingPoints(2, 2);
            points.Value = 9;
            Assert.AreEqual(2, points.CurrentLevel);
            Assert.AreEqual(15, points.PointsForNextLevel);
            Assert.IsTrue(points.Progress != 0);

            Assert.AreEqual("9/15", game.LevelCollection.FindView<PointCounterWidgetView>().Points.Value);
            Assert.AreEqual(points.Progress, game.LevelCollection.FindView<PointCounterWidgetView>().PointsProgress.MainValue);
            Assert.AreEqual(2, game.LevelCollection.FindViews<UnitView>().Count);

            game.Dispose();
        }

        [Test]
        public void IsQueuePositioningIsRight()
        {
            var game = new GameConstructor()
                .UpdateDefinition<ConstructionsSettingsDefinition>(c => c.CellSize = 1)
                .UpdateDefinition<ConstructionDefinition>(x => x.Points = 3)
                .UpdateDefinition<LevelDefinitionMock>((d) => d.CrowdUnitsAmount = 0)
                .UpdateDefinition<UnitsSettingsDefinition>(x => x.UnitSize = 0)
                .Build();

            Assert.AreEqual(0, game.LevelCollection.FindViews<UnitView>().Count);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.AreEqual(1, game.LevelCollection.FindViews<UnitView>().Count);
            Assert.AreEqual(1, game.LevelCollection.FindViews<ConstructionView>().Count);

            var building = game.LevelCollection.FindView<ConstructionView>();
            var unit = game.LevelCollection.FindView<UnitView>();

            Assert.AreEqual(building.Position.Value.X + 0.5f, unit.Position.Value.X);

            game.Dispose();
        }


        [Test]
        public void IsMultipleUnitsPositionsIsRight()
        {
            var uc = new UnitControllerMock();
            uc.SettingsDef.UnitSize = 1;
            var cr = new CrowdMock();
            var queue = new CustomerQueue(uc, uc, cr);

            Assert.AreEqual(0, uc.Units.Count);
            Assert.AreEqual(0, cr.Units.Count);

            uc.QueueSize = 1;
            queue.ServeCustomer();

            Assert.AreEqual(1, uc.Units.Count);
            Assert.AreEqual(new FloatPoint(1, 0), uc.Units.First().Position);
            Assert.AreEqual(new FloatPoint(0, 0), uc.Units.First().Target);
            Assert.AreEqual(0, cr.Units.Count);

            uc.QueueSize = 1;
            queue.ServeCustomer();

            Assert.AreEqual(2, uc.Units.Count);
            Assert.AreEqual(new FloatPoint(1, 0), uc.Units.First().Position);
            Assert.AreEqual(new FloatPoint(0, 0), uc.Units.First().Target);
            Assert.AreEqual(1, cr.Units.Count);

            uc.QueueSize = 2;
            queue.ServeCustomer();

            Assert.AreEqual(4, uc.Units.Count);
            Assert.AreEqual(new FloatPoint(1, 0), uc.Units.First().Position);
            Assert.AreEqual(new FloatPoint(0, 0), uc.Units.First().Target);
            Assert.AreEqual(new FloatPoint(2, 0), uc.Units.Last().Position);
            Assert.AreEqual(new FloatPoint(1, 0), uc.Units.Last().Target);
            Assert.AreEqual(2, cr.Units.Count);

            uc.QueueSize = 2;
            queue.ServeCustomer();

            Assert.AreEqual(5, uc.Units.Count);
            Assert.AreEqual(new FloatPoint(1, 0), uc.Units.First().Position);
            Assert.AreEqual(new FloatPoint(0, 0), uc.Units.First().Target);
            Assert.AreEqual(new FloatPoint(2, 0), uc.Units.Last().Position);
            Assert.AreEqual(new FloatPoint(1, 0), uc.Units.Last().Target);
            Assert.AreEqual(3, cr.Units.Count);

            queue.Dispose();
            uc.Dispose();
        }
        #endregion

        class UnitControllerMock : Disposable, IUnits, ICustomers
        {
            public CustomerDefinition Def { get; } = new CustomerDefinition();
            public UnitsSettingsDefinition SettingsDef { get; } = new UnitsSettingsDefinition();
            public SessionRandom Random { get; } = new SessionRandom();
            public List<Unit> Units = new List<Unit>();
            public int QueueSize { get; set; }

            public FloatPoint GetQueueFirstPosition()
            {
                return new FloatPoint(0, 0);
            }

            public float GetUnitSize()
            {
                return 1;
            }

            public Unit SpawnUnit(FloatPoint pos)
            {
                var unit = new Unit(pos, pos, Def, SettingsDef, Random);
                Units.Add(unit);
                return unit;
            }

            public void DestroyUnit(Unit unit)
            {
                Units.Remove(unit);
                unit.Dispose();
            }

            protected override void DisposeInner()
            {
                foreach (var uni in Units)
                {
                    uni.Dispose();
                }
            }

            public int GetQueueSize()
            {
                return QueueSize;
            }
        }

        class CrowdMock : ICrowd
        {
            public List<Unit> Units = new List<Unit>();
            public void SendToCrowd(Unit unit, LevelCrowd.CrowdDirection direction)
            {
                Units.Add(unit);
            }
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
