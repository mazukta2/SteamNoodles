using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Setups;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using Game.Assets.Scripts.Tests.Views.Level.Units;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Tests.Definitions;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Variations;

namespace Game.Assets.Scripts.Tests.Cases.Game.Customers
{
    public class CustomersQueueTests
    {

        [Test]
        public void IsProgressRight()
        {
            var time = new GameTime();
            var settings = new ConstructionsSettingsDefinition();
            var points = new BuildingPointsManager(settings, time, 2.2f, 8);
            var ghost = new GhostMock();

            var levelCollection = new ViewsCollection();
            var view = new PointCounterWidgetView(levelCollection);
            IPointCounterWidgetView.Default = view;
            var spawner = new PieceSpawnerView(levelCollection);
            new PointPieceSpawnerPresenter(spawner);
            new PointCounterWidgetPresenter(points, ghost, time, view, spawner, settings);

            points.Change(1);
            Assert.AreEqual(1/9f, view.PointsProgress.MainValue);
            points.Change(8);
            Assert.AreEqual(0, view.PointsProgress.MainValue);
            points.Change(1);

            Assert.AreEqual(20, points.PointsForNextLevel);
            Assert.AreEqual(10, points.Value);
            Assert.AreEqual(9, points.PointsForCurrentLevel);
            Assert.AreEqual(1 / 11f, points.Progress);

            Assert.AreEqual(1/11f, view.PointsProgress.MainValue);
            points.Change(10);
            Assert.AreEqual(20, points.Value);
            Assert.AreEqual(20, points.PointsForCurrentLevel);
            Assert.AreEqual(0, points.Progress);
            Assert.AreEqual(0, view.PointsProgress.MainValue);

            levelCollection.Dispose();
            points.Dispose();
        }

        public class GhostMock : IGhostPresenter
        {
            public event Action OnGhostChanged = delegate { };
            public event Action OnGhostPostionChanged = delegate { };
            public int GetPointChanges() => 0;
        }

        [Test]
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

        [Test]
        public void IsPointsConvertsToQueueSize()
        {
            var game = new GameConstructor()
                .UpdateDefinition<ConstructionsSettingsDefinition>(c => c.CellSize = 1)
                .UpdateDefinition<ConstructionDefinition>(x => x.Points = 9)
                .UpdateDefinition<LevelDefinitionMock>((d) => d.MainLevelVariation.CrowdUnitsAmount = 0)
                .Build();

            Assert.AreEqual("0/3", game.LevelCollection.FindView<PointCounterWidgetView>().Points.Value);
            Assert.AreEqual(0, game.LevelCollection.FindView<PointCounterWidgetView>().PointsProgress.MainValue);
            Assert.AreEqual(0, game.LevelCollection.FindViews<UnitView>().Count);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            var points = new BuildingPointsCalculator(2, 2);
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
                .UpdateDefinition<LevelDefinitionMock>((d) => d.MainLevelVariation.CrowdUnitsAmount = 0)
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
            var queue = new CustomerQueue(uc, uc, cr, uc.Time, uc.Random);

            Assert.AreEqual(0, uc.Units.Count);
            Assert.AreEqual(0, cr.Units.Count);

            uc.QueueSize = 1;
            queue.ServeCustomer();

            Assert.AreEqual(0, cr.Units.Count);
            Assert.AreEqual(1, queue.Units.Count);
            Assert.AreEqual(1, uc.Units.Count);
            Assert.AreEqual(new GameVector3(1, 0, 0), uc.Units.First().Position);
            Assert.AreEqual(new GameVector3(0, 0, 0), uc.Units.First().Target);

            uc.QueueSize = 1;
            queue.ServeCustomer();
            uc.Time.MoveTime(1);

            Assert.AreEqual(1, cr.Units.Count);
            Assert.AreEqual(1, queue.Units.Count);
            Assert.AreEqual(2, uc.Units.Count);
            Assert.AreEqual(new GameVector3(0, 0, 0), uc.Units.First().Position);
            Assert.AreEqual(new GameVector3(0, 0, 0), uc.Units.First().Target);

            uc.QueueSize = 2;
            queue.ServeCustomer();
            uc.Time.MoveTime(1);

            Assert.AreEqual(2, cr.Units.Count);
            Assert.AreEqual(2, queue.Units.Count);
            Assert.AreEqual(4, uc.Units.Count);
            Assert.AreEqual(new GameVector3(0, 0, 0), uc.Units.First().Position);
            Assert.AreEqual(new GameVector3(0, 0, 0), uc.Units.First().Target);
            Assert.AreEqual(new GameVector3(2, 0, 0), uc.Units.Last().Position);
            Assert.AreEqual(new GameVector3(1, 0, 0), uc.Units.Last().Target);

            uc.QueueSize = 2;
            queue.ServeCustomer();
            uc.Time.MoveTime(1);

            Assert.AreEqual(3, cr.Units.Count);
            Assert.AreEqual(2, queue.Units.Count);
            Assert.AreEqual(5, uc.Units.Count);
            Assert.AreEqual(new GameVector3(0, 0, 0), uc.Units.First().Position);
            Assert.AreEqual(new GameVector3(0, 0, 0), uc.Units.First().Target);
            Assert.AreEqual(new GameVector3(2, 0, 0), uc.Units.Last().Position);
            Assert.AreEqual(new GameVector3(1, 0, 0), uc.Units.Last().Target);

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
            uc.FirstPositionOffset = new GameVector3(0, 0, 1);

            IGameTime.Default = uc.Time;
            var cr = new CrowdMock();
            var queue = new CustomerQueue(uc, uc, cr, uc.Time, uc.Random);

            var collection = new ViewsCollection();
            new UnitsPresenter(uc, new UnitsManagerView(collection), uc.SettingsDef);
 
            uc.QueueSize = 2;
            queue.ServeCustomer();

            Assert.AreEqual(new GameVector3(1, 0, 0), uc.Units[0].Position);
            Assert.AreEqual(new GameVector3(2, 0, 0), uc.Units[1].Position);

            var views = collection.FindViews<UnitView>().ToList();
            var originalRotation = views[0].Rotator.Rotation;
            Assert.AreEqual(new GameVector3(-1, 0, 0), views[0].Rotator.Rotation.ToVector().GetRound());
            Assert.AreEqual(new GameVector3(-1, 0, 0), views[1].Rotator.Rotation.ToVector().GetRound());

            var timePassed = 0.1f;
            uc.Time.MoveTime(timePassed);

            var firstUnitTraget = uc.GetQueueFirstPosition() + uc.FirstPositionOffset;
            Assert.AreEqual(firstUnitTraget, uc.Units[0].Target);
            Assert.AreEqual(0.6f, uc.Units[0].GetCurrentSpeed());
            var targetRotation = (firstUnitTraget - new GameVector3(1, 0, 0)).ToQuaternion();
            
            Assert.AreEqual(new GameVector3(1, 0, 0).MoveTowards(firstUnitTraget, 0.6f * timePassed), uc.Units[0].Position);
            Assert.AreEqual(new GameVector3(1.95f, 0, 0), uc.Units[1].Position);
            AssertHelpers.CompareVectors(GameQuaternion.RotateTowards(originalRotation, targetRotation, 0.5f * timePassed).ToVector(), 
                views[0].Rotator.Rotation.ToVector(), 0.01f);
            Assert.AreEqual(new GameVector3(-1, 0, 0), views[1].Rotator.Rotation.ToVector().GetRound());

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
            level.MainLevelVariation.UnitsRect = new FloatRect(-10, -4, 20, 8);
            level.MainLevelVariation.CrowdUnitsAmount = 0;
            var crowd = new LevelCrowd(uc, time, level.MainLevelVariation, uc.Random);

            var unit = uc.SpawnUnit(new GameVector3(0, 0, 0));
            crowd.SendToCrowd(unit, LevelCrowd.CrowdDirection.Left);
            Assert.AreEqual(new GameVector3(0, 0, 0), unit.Position);
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

            var unit2 = uc.SpawnUnit(new GameVector3(0, 0, 0));
            crowd.SendToCrowd(unit2, LevelCrowd.CrowdDirection.Right);
            Assert.AreEqual(new GameVector3(0, 0, 0), unit2.Position);
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

        [Test]
        public void IsQueueFreeAllWorking()
        {
            var uc = new UnitControllerMock();
            uc.SettingsDef = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            IGameTime.Default = uc.Time;
            var cr = new CrowdMock();
            var queue = new CustomerQueue(uc, uc, cr, uc.Time, uc.Random);

            var collection = new ViewsCollection();
            new UnitsPresenter(uc, new UnitsManagerView(collection), uc.SettingsDef);

            uc.QueueSize = 2;
            queue.ServeCustomer();
            uc.Time.MoveTime(1);

            Assert.AreEqual(2, collection.FindViews<UnitView>().Count());
            Assert.AreEqual(2, queue.Units.Count());
            Assert.AreEqual(0, cr.Units.Count);

            queue.FreeAll();

            Assert.AreEqual(2, collection.FindViews<UnitView>().Count());
            Assert.AreEqual(0, queue.Units.Count());
            Assert.AreEqual(2, cr.Units.Count);

            uc.QueueSize = 2;
            queue.ServeCustomer();
            uc.Time.MoveTime(1);

            Assert.AreEqual(4, collection.FindViews<UnitView>().Count());
            Assert.AreEqual(2, queue.Units.Count());
            Assert.AreEqual(2, cr.Units.Count);

            collection.Dispose();
            queue.Dispose();
            uc.Dispose();
        }

        [Test]
        public void IsServeAllWorking()
        {
            var uc = new UnitControllerMock();
            uc.SettingsDef = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            IGameTime.Default = uc.Time;
            var cr = new CrowdMock();
            var queue = new CustomerQueue(uc, uc, cr, uc.Time, uc.Random);

            var collection = new ViewsCollection();
            new UnitsPresenter(uc, new UnitsManagerView(collection), uc.SettingsDef);

            uc.QueueSize = 2;
            queue.ServeCustomer();
            uc.Time.MoveTime(1);

            var unitList = queue.Units.ToArray();
            Assert.AreEqual(2, collection.FindViews<UnitView>().Count());
            Assert.AreEqual(2, unitList.Count());
            Assert.AreEqual(0, cr.Units.Count);

            queue.ServeAll();
            uc.Time.MoveTime(1);

            Assert.AreEqual(4, collection.FindViews<UnitView>().Count());
            Assert.AreEqual(2, queue.Units.Count());
            Assert.AreEqual(2, cr.Units.Count);

            Assert.AreEqual(uc.Units[0], unitList[0]);
            Assert.AreEqual(uc.Units[1], unitList[1]);
            Assert.AreEqual(uc.Units[2], queue.Units.ToArray()[0]);
            Assert.AreEqual(uc.Units[3], queue.Units.ToArray()[1]);

            uc.QueueSize = 2;
            queue.ServeCustomer();
            uc.Time.MoveTime(1);

            Assert.AreEqual(5, collection.FindViews<UnitView>().Count());
            Assert.AreEqual(2, queue.Units.Count());
            Assert.AreEqual(3, cr.Units.Count);

            collection.Dispose();
            queue.Dispose();
            uc.Dispose();
        }

        [Test]
        public void IsServeAllTimerWorking()
        {
            var uc = new UnitControllerMock();
            uc.SettingsDef = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            IGameTime.Default = uc.Time;
            var cr = new CrowdMock();
            var queue = new CustomerQueue(uc, uc, cr, uc.Time, uc.Random);

            var collection = new ViewsCollection();
            new UnitsPresenter(uc, new UnitsManagerView(collection), uc.SettingsDef);

            uc.QueueSize = 2;
            queue.ServeCustomer();
            uc.Time.MoveTime(1);

            Assert.AreEqual(2, queue.Units.Count());

            uc.SpawnAnimationDelay = 1;
            queue.ServeAll();
            // first unit is server instantly
            Assert.AreEqual(1, queue.Units.Count());
            uc.Time.MoveTime(0.5f);
            Assert.AreEqual(1, queue.Units.Count());
            uc.Time.MoveTime(0.5f);

            // second unit - after first iteration
            Assert.AreEqual(0, queue.Units.Count());
            uc.Time.MoveTime(0.5f);
            Assert.AreEqual(0, queue.Units.Count());
            uc.Time.MoveTime(0.5f);

            // swawn first unit
            Assert.AreEqual(1, queue.Units.Count());
            uc.Time.MoveTime(0.5f);
            Assert.AreEqual(1, queue.Units.Count());
            uc.Time.MoveTime(0.5f);

            // swawn second unit
            Assert.AreEqual(2, queue.Units.Count());

            collection.Dispose();
            queue.Dispose();
            uc.Dispose();
        }

        [Test]
        public void IsServeAllPositionsWorking()
        {
            var uc = new UnitControllerMock();
            uc.SettingsDef = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            IGameTime.Default = uc.Time;
            var cr = new CrowdMock();
            var queue = new CustomerQueue(uc, uc, cr, uc.Time, uc.Random);
            uc.FirstPositionOffset = new GameVector3(0, 0, 1);
            uc.SettingsDef.Speed = 1;
            uc.SettingsDef.SpeedUp = 1;
            uc.SettingsDef.SpeedUpDistance = 1;
            uc.SettingsDef.MinSpeed = 1f;
            uc.SettingsDef.RotationSpeed = 1f;

            var collection = new ViewsCollection();
            new UnitsPresenter(uc, new UnitsManagerView(collection), uc.SettingsDef);

            uc.QueueSize = 4;
            queue.ServeCustomer();
            uc.Time.MoveTime(2);

            Assert.AreEqual(4, queue.Units.Count());

            uc.SpawnAnimationDelay = 1;
            queue.ServeAll();
            // 1
            Assert.AreEqual(3, queue.Units.Count());
            Assert.AreEqual(new GameVector3(1, 0, 0), queue.Units.ToArray()[0].Position);
            Assert.AreEqual(new GameVector3(2, 0, 0), queue.Units.ToArray()[1].Position);
            Assert.AreEqual(new GameVector3(3, 0, 0), queue.Units.ToArray()[2].Position);
            uc.Time.MoveTime(1f);
            // 2
            Assert.AreEqual(2, queue.Units.Count());
            Assert.AreEqual(new GameVector3(2, 0, 0), queue.Units.ToArray()[0].Position);
            Assert.AreEqual(new GameVector3(3, 0, 0), queue.Units.ToArray()[1].Position);
            uc.Time.MoveTime(1f);
            // 3
            Assert.AreEqual(1, queue.Units.Count());
            Assert.AreEqual(new GameVector3(3, 0, 0), queue.Units.ToArray()[0].Position);
            uc.Time.MoveTime(1f);
            // 4
            Assert.AreEqual(0, queue.Units.Count());
            collection.Dispose();
            queue.Dispose();
            uc.Dispose();
        }

        [Test]
        public void IsServeCoinsWorking()
        {
            var uc = new UnitControllerMock();
            uc.SettingsDef = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            IGameTime.Default = uc.Time;
            var cr = new CrowdMock();
            var queue = new CustomerQueue(uc, uc, cr, uc.Time, uc.Random);

            var collection = new ViewsCollection();
            new UnitsPresenter(uc, new UnitsManagerView(collection), uc.SettingsDef);

            Assert.AreEqual(0, uc.Coins);

            uc.QueueSize = 2;
            queue.ServeCustomer(); // add new one
            queue.ServeCustomer(); // serve
            uc.Time.MoveTime(1);

            Assert.AreEqual(1, uc.Coins);

            collection.Dispose();
            queue.Dispose();
            uc.Dispose();
        }

        [Test]
        public void IsFirstUnitAppearsAfterFirstBuilding()
        {
            var game = new GameConstructor()
                .UpdateDefinition<ConstructionsSettingsDefinition>(x => x.PieceMovingTime = 1)
                .UpdateDefinition<ConstructionDefinition>(x => x.Points = 3)
                .UpdateDefinition<LevelDefinitionMock>(x => x.MainLevelVariation.CrowdUnitsAmount = 0)
                .Build();

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.AreEqual(1, IMainLevel.Default.Resources.Points.TargetLevel);
            Assert.AreEqual(0, IMainLevel.Default.Resources.Points.CurrentLevel);
            Assert.AreEqual(1, game.LevelCollection.FindViews<UnitView>().Count);

            game.Dispose();
        }

        class UnitControllerMock : Disposable, IUnits, ICustomers
        {
            public CustomerDefinition Def { get; } = new CustomerDefinition();
            public UnitsSettingsDefinition SettingsDef { get; set; } = new UnitsSettingsDefinition();
            public SessionRandom Random { get; } = new SessionRandom();
            public List<Unit> Units = new List<Unit>();

            public event Action<Unit> OnUnitSpawn = delegate { };

            public GameVector3 FirstPositionOffset { get; set; } = GameVector3.Zero;

            public int Coins { get; set; }

            public int QueueSize { get; set; }
            public GameTime Time { get; set; } = new GameTime();

            IReadOnlyCollection<Unit> IUnits.Units => Units.AsReadOnly();

            public float SpawnAnimationDelay { get; set; }

            public GameVector3 GetQueueFirstPosition()
            {
                return new GameVector3(0, 0, 0);
            }

            public GameVector3 GetQueueFirstPositionOffset()
            {
                return FirstPositionOffset;
            }

            public float GetUnitSize()
            {
                return 1;
            }

            public Unit SpawnUnit(GameVector3 pos)
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

            public void Serve(Unit unit)
            {
                Coins++;
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
