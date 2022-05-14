using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Setups;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using Game.Assets.Scripts.Tests.Views.Level.Units;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Tests.Cases;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Game.Customers
{
    public class CustomersQueueTests
    {

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

            Assert.AreEqual(building.Position.Value.X, unit.Position.Value.X);

            game.Dispose();
        }


        [Test]
        public void IsMultipleUnitsPositionsIsRight()
        {
            var uc = new UnitControllerMock();
            uc.SettingsDef = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            var cr = new CrowdMock();
            var queue = new CustomerQueue(uc, uc, cr);

            Assert.AreEqual(0, uc.Units.Count);
            Assert.AreEqual(0, cr.Units.Count);

            uc.QueueSize = 1;
            queue.ServeCustomer();

            Assert.AreEqual(1, uc.Units.Count);
            Assert.AreEqual(new FloatPoint3D(1, 0, 0), uc.Units.First().Position);
            Assert.AreEqual(new FloatPoint3D(0, 0, 0), uc.Units.First().Target);
            Assert.AreEqual(0, cr.Units.Count);

            uc.QueueSize = 1;
            queue.ServeCustomer();

            Assert.AreEqual(2, uc.Units.Count);
            Assert.AreEqual(new FloatPoint3D(1, 0, 0), uc.Units.First().Position);
            Assert.AreEqual(new FloatPoint3D(0, 0, 0), uc.Units.First().Target);
            Assert.AreEqual(1, cr.Units.Count);

            uc.QueueSize = 2;
            queue.ServeCustomer();

            Assert.AreEqual(4, uc.Units.Count);
            Assert.AreEqual(new FloatPoint3D(1, 0, 0), uc.Units.First().Position);
            Assert.AreEqual(new FloatPoint3D(0, 0, 0), uc.Units.First().Target);
            Assert.AreEqual(new FloatPoint3D(2, 0, 0), uc.Units.Last().Position);
            Assert.AreEqual(new FloatPoint3D(1, 0, 0), uc.Units.Last().Target);
            Assert.AreEqual(2, cr.Units.Count);

            uc.QueueSize = 2;
            queue.ServeCustomer();

            Assert.AreEqual(5, uc.Units.Count);
            Assert.AreEqual(new FloatPoint3D(1, 0, 0), uc.Units.First().Position);
            Assert.AreEqual(new FloatPoint3D(0, 0, 0), uc.Units.First().Target);
            Assert.AreEqual(new FloatPoint3D(2, 0, 0), uc.Units.Last().Position);
            Assert.AreEqual(new FloatPoint3D(1, 0, 0), uc.Units.Last().Target);
            Assert.AreEqual(3, cr.Units.Count);

            queue.Dispose();
            uc.Dispose();
        }

        [Test]
        public void IsQueueVisualRight()
        {
            var uc = new UnitControllerMock();
            uc.SettingsDef = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            uc.SettingsDef.Speed = 1;
            uc.SettingsDef.SpeedUp = 1;
            uc.SettingsDef.SpeedUpDistance = 1;
            uc.SettingsDef.MinSpeed = 0.5f;
            uc.SettingsDef.RotationSpeed = 0.5f;
            uc.FirstPositionOffset = new FloatPoint3D(0, 0, 1);

            IGameTime.Default = uc.Time;
            var cr = new CrowdMock();
            var queue = new CustomerQueue(uc, uc, cr);

            var collection = new ViewsCollection();
            new UnitsPresenter(uc, new UnitsManagerView(collection), uc.SettingsDef);
 
            uc.QueueSize = 2;
            queue.ServeCustomer();

            Assert.AreEqual(new FloatPoint3D(1, 0, 0), uc.Units[0].Position);
            Assert.AreEqual(new FloatPoint3D(2, 0, 0), uc.Units[1].Position);

            var views = collection.FindViews<UnitView>().ToList();
            var originalRotation = views[0].Rotator.Rotation;
            Assert.AreEqual(new FloatPoint3D(-1, 0, 0), views[0].Rotator.Rotation.ToVector().GetRound());
            Assert.AreEqual(new FloatPoint3D(-1, 0, 0), views[1].Rotator.Rotation.ToVector().GetRound());

            var timePassed = 0.1f;
            uc.Time.MoveTime(timePassed);

            var firstUnitTraget = uc.GetQueueFirstPosition() + uc.FirstPositionOffset;
            Assert.AreEqual(firstUnitTraget, uc.Units[0].Target);
            Assert.AreEqual(0.6f, uc.Units[0].GetCurrentSpeed());
            var targetRotation = (firstUnitTraget - new FloatPoint3D(1, 0, 0)).ToQuaternion();
            
            Assert.AreEqual(new FloatPoint3D(1, 0, 0).MoveTowards(firstUnitTraget, 0.6f * timePassed), uc.Units[0].Position);
            Assert.AreEqual(new FloatPoint3D(1.95f, 0, 0), uc.Units[1].Position);
            AssertHelpers.CompareVectors(GameQuaternion.RotateTowards(originalRotation, targetRotation, 0.5f * timePassed).ToVector(), 
                views[0].Rotator.Rotation.ToVector(), 0.01f);
            Assert.AreEqual(new FloatPoint3D(-1, 0, 0), views[1].Rotator.Rotation.ToVector().GetRound());

            collection.Dispose();
            queue.Dispose();
            uc.Dispose();
        }

        [Test]
        public void IsLevelCrowdGetUnitsCorrectly()
        {
            var uc = new UnitControllerMock();
            uc.SettingsDef = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            var time = uc.Time;
            var level = LevelDefinitionSetups.GetDefault();
            level.UnitsRect = new FloatRect(-10, -4, 20, 8);
            level.CrowdUnitsAmount = 0;
            var crowd = new LevelCrowd(uc, time, level, uc.Random);

            var unit = uc.SpawnUnit(new FloatPoint3D(0, 0, 0));
            crowd.SendToCrowd(unit, LevelCrowd.CrowdDirection.Left);
            Assert.AreEqual(new FloatPoint3D(0, 0, 0), unit.Position);
            Assert.AreEqual(-10, unit.Target.X);
            Assert.AreEqual(0, unit.Target.Y);
            Assert.GreaterOrEqual(unit.Target.Z, - 4);
            Assert.LessOrEqual(unit.Target.Z, 4);
            Assert.IsFalse(unit.IsDisposed);
            time.MoveTime(1);
            Assert.IsFalse(unit.IsDisposed);
            time.MoveTime(100);
            Assert.AreEqual(unit.Target, unit.Position);
            Assert.IsTrue(unit.IsDisposed);

            var unit2 = uc.SpawnUnit(new FloatPoint3D(0, 0, 0));
            crowd.SendToCrowd(unit2, LevelCrowd.CrowdDirection.Right);
            Assert.AreEqual(new FloatPoint3D(0, 0, 0), unit2.Position);
            Assert.AreEqual(10, unit2.Target.X);
            Assert.AreEqual(0, unit2.Target.Y);
            Assert.GreaterOrEqual(unit2.Target.Z, -4);
            Assert.LessOrEqual(unit2.Target.Z, 4);
            Assert.IsFalse(unit2.IsDisposed);
            time.MoveTime(1);
            Assert.IsFalse(unit2.IsDisposed);
            time.MoveTime(100);
            Assert.AreEqual(unit2.Target, unit2.Position);
            Assert.IsTrue(unit2.IsDisposed);

            crowd.Dispose();
            uc.Dispose();
        }

        class UnitControllerMock : Disposable, IUnits, ICustomers
        {
            public CustomerDefinition Def { get; } = new CustomerDefinition();
            public UnitsSettingsDefinition SettingsDef { get; set; } = new UnitsSettingsDefinition();
            public SessionRandom Random { get; } = new SessionRandom();
            public List<Unit> Units = new List<Unit>();

            public event Action<Unit> OnUnitSpawn = delegate { };

            public FloatPoint3D FirstPositionOffset { get; set; } = FloatPoint3D.Zero;

            public int QueueSize { get; set; }
            public GameTime Time { get; set; } = new GameTime();

            IReadOnlyCollection<Unit> IUnits.Units => Units.AsReadOnly();

            public FloatPoint3D GetQueueFirstPosition()
            {
                return new FloatPoint3D(0, 0, 0);
            }

            public FloatPoint3D GetQueueFirstPositionOffset()
            {
                return FirstPositionOffset;
            }

            public float GetUnitSize()
            {
                return 1;
            }

            public Unit SpawnUnit(FloatPoint3D pos)
            {
                var unit = new Unit(pos, pos, Def, SettingsDef, Random, Time);
                Units.Add(unit);
                OnUnitSpawn(unit);
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
