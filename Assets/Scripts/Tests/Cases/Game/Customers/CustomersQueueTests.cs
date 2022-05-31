using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Types;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions;
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
                .UpdateDefinition<LevelDefinitionMock>((d) => d.CrowdUnitsAmount = 0)
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
            var unitsRepository = new Repository<Unit>();
            var time = new GameTime();
            var level = LevelDefinitionSetups.GetDefault();
            var random = new SessionRandom();
            var uc = new UnitsService(unitsRepository, UnitDefinitionSetup.GetDefaultUnitsDefinitions(), level, random);

            var crowd = new UnitsCrowdService(unitsRepository, uc, time, level, random);
            var queue = new UnitsCustomerQueueService(unitsRepository, uc, crowd, time, random);

            Assert.AreEqual(0, unitsRepository.Count);
            Assert.AreEqual(0, crowd.GetUnits().Count);

            queue.SetQueueSize(1);
            queue.ServeCustomer();

            Assert.AreEqual(0, crowd.GetUnits().Count);
            Assert.AreEqual(1, queue.GetQueueUnits().Count);
            Assert.AreEqual(1, unitsRepository.Count);
            Assert.AreEqual(new GameVector3(1, 0, 0), unitsRepository.Get().First().Position);
            Assert.AreEqual(new GameVector3(0, 0, 0), unitsRepository.Get().First().Target);

            queue.SetQueueSize(1);
            queue.ServeCustomer();
            time.MoveTime(1);

            Assert.AreEqual(1, crowd.GetUnits().Count);
            Assert.AreEqual(1, queue.GetQueueUnits().Count);
            Assert.AreEqual(2, unitsRepository.Count);
            Assert.AreEqual(new GameVector3(0, 0, 0), unitsRepository.Get().First().Position);
            Assert.AreEqual(new GameVector3(0, 0, 0), unitsRepository.Get().First().Target);

            queue.SetQueueSize(2);
            queue.ServeCustomer();
            time.MoveTime(1);

            Assert.AreEqual(2, crowd.GetUnits().Count);
            Assert.AreEqual(2, queue.GetQueueUnits().Count);
            Assert.AreEqual(4, unitsRepository.Count);
            Assert.AreEqual(new GameVector3(0, 0, 0), unitsRepository.Get().First().Position);
            Assert.AreEqual(new GameVector3(0, 0, 0), unitsRepository.Get().First().Target);
            Assert.AreEqual(new GameVector3(2, 0, 0), unitsRepository.Get().Last().Position);
            Assert.AreEqual(new GameVector3(1, 0, 0), unitsRepository.Get().Last().Target);

            queue.SetQueueSize(2);
            queue.ServeCustomer();
            time.MoveTime(1);

            Assert.AreEqual(3, crowd.GetUnits().Count);
            Assert.AreEqual(2, queue.GetQueueUnits().Count);
            Assert.AreEqual(5, unitsRepository.Count);
            Assert.AreEqual(new GameVector3(0, 0, 0), unitsRepository.Get().First().Position);
            Assert.AreEqual(new GameVector3(0, 0, 0), unitsRepository.Get().First().Target);
            Assert.AreEqual(new GameVector3(2, 0, 0), unitsRepository.Get().Last().Position);
            Assert.AreEqual(new GameVector3(1, 0, 0), unitsRepository.Get().Last().Target);

            queue.Dispose();
            uc.Dispose();
        }

        [Test]
        public void IsQueueVisualRight()
        {
            var unitsRepository = new Repository<Unit>();
            var time = new GameTime();
            var level = LevelDefinitionSetups.GetDefault();
            var random = new SessionRandom();
            var unitSettings = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            unitSettings.Speed = 1;
            unitSettings.SpeedUp = 1;
            unitSettings.SpeedUpDistance = 1;
            unitSettings.MinSpeed = 0.5f;
            unitSettings.RotationSpeed = 0.5f;
            
            var uc = new UnitsService(unitsRepository, unitSettings, level, random);

            IGameTime.Default = time;
            var crowd = new UnitsCrowdService(unitsRepository, uc, time, level, random);
            var queue = new UnitsCustomerQueueService(unitsRepository, uc, crowd, time, random, new GameVector3(0, 0, 1));

            var collection = new ViewsCollection();
            new UnitsPresenter(unitsRepository, new UnitsManagerView(collection), unitSettings);

            queue.SetQueueSize(2);
            queue.ServeCustomer();

            Assert.AreEqual(new GameVector3(1, 0, 0), queue.GetUnits().ToArray()[0].Position);
            Assert.AreEqual(new GameVector3(2, 0, 0), queue.GetUnits().ToArray()[1].Position);

            var views = collection.FindViews<UnitView>().ToList();
            var originalRotation = views[0].Rotator.Rotation;
            Assert.AreEqual(new GameVector3(-1, 0, 0), views[0].Rotator.Rotation.ToVector().GetRound());
            Assert.AreEqual(new GameVector3(-1, 0, 0), views[1].Rotator.Rotation.ToVector().GetRound());

            var timePassed = 0.1f;
            time.MoveTime(timePassed);
            
            var firstUnitTraget = queue.GetQueueFirstPosition() + queue.GetQueueFirstPositionOffset();
            Assert.AreEqual(firstUnitTraget, queue.GetUnits().ToArray()[0].Target);
            Assert.AreEqual(0.6f, queue.GetUnits().ToArray()[0].CurrentSpeed);
            var targetRotation = (firstUnitTraget - new GameVector3(1, 0, 0)).ToQuaternion();
            
            Assert.AreEqual(new GameVector3(1, 0, 0).MoveTowards(firstUnitTraget, 0.6f * timePassed), queue.GetUnits().ToArray()[0].Position);
            Assert.AreEqual(new GameVector3(1.95f, 0, 0), queue.GetUnits().ToArray()[1].Position);
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
            var unitsRepository = new Repository<Unit>();
            var time = new GameTime();
            var level = LevelDefinitionSetups.GetDefault();
            level.UnitsRect = new FloatRect(-10, -4, 20, 8);
            level.CrowdUnitsAmount = 0;
            var random = new SessionRandom();
            var uc = new UnitsService(unitsRepository, UnitDefinitionSetup.GetDefaultUnitsDefinitions(), level, random);

            var crowdRepository = new Repository<Unit>();
            var crowd = new UnitsCrowdService(crowdRepository, uc, time, level, random);

            var unit = uc.SpawnUnit(new GameVector3(0, 0, 0));
            crowd.SendToCrowd(unit, UnitsCrowdService.CrowdDirection.Left);
            Assert.AreEqual(new GameVector3(0, 0, 0), unit.Position);
            Assert.AreEqual(-10, unit.Target.X);
            Assert.AreEqual(0, unit.Target.Y);
            Assert.GreaterOrEqual(unit.Target.Z, - 4);
            Assert.LessOrEqual(unit.Target.Z, 4);
            Assert.IsNotNull(unitsRepository.Get(unit.Id));
            time.MoveTime(1);
            Assert.IsNotNull(unitsRepository.Get(unit.Id));
            time.MoveTime(100);
            Assert.AreEqual(unit.Target, unit.Position);
            Assert.IsNull(unitsRepository.Get(unit.Id));

            var unit2 = uc.SpawnUnit(new GameVector3(0, 0, 0));
            crowd.SendToCrowd(unit2, UnitsCrowdService.CrowdDirection.Right);
            Assert.AreEqual(new GameVector3(0, 0, 0), unit2.Position);
            Assert.AreEqual(10, unit2.Target.X);
            Assert.AreEqual(0, unit2.Target.Y);
            Assert.GreaterOrEqual(unit2.Target.Z, -4);
            Assert.LessOrEqual(unit2.Target.Z, 4);
            Assert.IsNotNull(unitsRepository.Get(unit2.Id));
            time.MoveTime(1);
            Assert.IsNotNull(unitsRepository.Get(unit2.Id));
            time.MoveTime(100);
            Assert.AreEqual(unit2.Target, unit2.Position);
            Assert.IsNull(unitsRepository.Get(unit2.Id));

            crowd.Dispose();
            uc.Dispose();
        }

        [Test]
        public void IsQueueFreeAllWorking()
        {
            var unitsRepository = new Repository<Unit>();
            var time = new GameTime();
            var level = LevelDefinitionSetups.GetDefault();
            var random = new SessionRandom();
            var unitSettings = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            IGameTime.Default = time;

            var uc = new UnitsService(unitsRepository, unitSettings, level, random);

            var crowd = new UnitsCrowdService(unitsRepository, uc, time, level, random);
            var queue = new UnitsCustomerQueueService(unitsRepository, uc, crowd, time, random, new GameVector3(0, 0, 1));

            var collection = new ViewsCollection();
            new UnitsPresenter(unitsRepository, new UnitsManagerView(collection), unitSettings);

            queue.SetQueueSize(2);
            queue.ServeCustomer();
            time.MoveTime(1);

            Assert.AreEqual(2, collection.FindViews<UnitView>().Count());
            Assert.AreEqual(2, queue.GetUnits().Count());
            Assert.AreEqual(0, crowd.GetUnits().Count);

            queue.FreeAll();

            Assert.AreEqual(2, collection.FindViews<UnitView>().Count());
            Assert.AreEqual(0, queue.GetUnits().Count());
            Assert.AreEqual(2, crowd.GetUnits().Count);

            queue.SetQueueSize(2);
            queue.ServeCustomer();
            time.MoveTime(1);

            Assert.AreEqual(4, collection.FindViews<UnitView>().Count());
            Assert.AreEqual(2, queue.GetUnits().Count());
            Assert.AreEqual(2, crowd.GetUnits().Count);

            collection.Dispose();
            queue.Dispose();
            uc.Dispose();
        }

        [Test]
        public void IsServeAllWorking()
        {
            var unitsRepository = new Repository<Unit>();
            var time = new GameTime();
            var level = LevelDefinitionSetups.GetDefault();
            var random = new SessionRandom();
            var unitSettings = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            IGameTime.Default = time;

            var uc = new UnitsService(unitsRepository, unitSettings, level, random);
            var crowd = new UnitsCrowdService(unitsRepository, uc, time, level, random);
            var queue = new UnitsCustomerQueueService(unitsRepository, uc, crowd, time, random, new GameVector3(0, 0, 1));


            var collection = new ViewsCollection();
            new UnitsPresenter(unitsRepository, new UnitsManagerView(collection), unitSettings);

            queue.SetQueueSize(2);
            queue.ServeCustomer();
            time.MoveTime(1);

            var unitList = queue.GetUnits().ToArray();
            Assert.AreEqual(2, collection.FindViews<UnitView>().Count());
            Assert.AreEqual(2, unitList.Count());
            Assert.AreEqual(0, crowd.GetUnits().Count);

            queue.ServeAll();
            time.MoveTime(1);

            Assert.AreEqual(4, collection.FindViews<UnitView>().Count());
            Assert.AreEqual(2, queue.GetUnits().Count());
            Assert.AreEqual(2, crowd.GetUnits().Count);

            Assert.AreEqual(unitsRepository.Get().ElementAt(0), unitList[0]);
            Assert.AreEqual(unitsRepository.Get().ElementAt(1), unitList[1]);
            Assert.AreEqual(unitsRepository.Get().ElementAt(2), queue.GetUnits().ElementAt(0));
            Assert.AreEqual(unitsRepository.Get().ElementAt(3), queue.GetUnits().ElementAt(1));

            queue.SetQueueSize(2);
            queue.ServeCustomer();
            time.MoveTime(1);

            Assert.AreEqual(5, collection.FindViews<UnitView>().Count());
            Assert.AreEqual(2, queue.GetUnits().Count());
            Assert.AreEqual(3, crowd.GetUnits().Count);

            collection.Dispose();
            queue.Dispose();
            uc.Dispose();
        }

        [Test]
        public void IsServeAllTimerWorking()
        {
            var unitsRepository = new Repository<Unit>();
            var time = new GameTime();
            var level = LevelDefinitionSetups.GetDefault();
            var random = new SessionRandom();
            var unitSettings = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            IGameTime.Default = time;

            var uc = new UnitsService(unitsRepository, unitSettings, level, random);
            var crowd = new UnitsCrowdService(unitsRepository, uc, time, level, random);
            var queue = new UnitsCustomerQueueService(unitsRepository, uc, crowd, time, random, new GameVector3(0, 0, 1), 1);

            var collection = new ViewsCollection();
            new UnitsPresenter(unitsRepository, new UnitsManagerView(collection), unitSettings);

            queue.SetQueueSize(2);
            queue.ServeCustomer();
            time.MoveTime(1);

            Assert.AreEqual(2, queue.GetUnits().Count());

            queue.ServeAll();
            // first unit is server instantly
            Assert.AreEqual(1, queue.GetUnits().Count());
            time.MoveTime(0.5f);
            Assert.AreEqual(1, queue.GetUnits().Count());
            time.MoveTime(0.5f);

            // second unit - after first iteration
            Assert.AreEqual(0, queue.GetUnits().Count());
            time.MoveTime(0.5f);
            Assert.AreEqual(0, queue.GetUnits().Count());
            time.MoveTime(0.5f);

            // swawn first unit
            Assert.AreEqual(1, queue.GetUnits().Count());
            time.MoveTime(0.5f);
            Assert.AreEqual(1, queue.GetUnits().Count());
            time.MoveTime(0.5f);

            // swawn second unit
            Assert.AreEqual(2, queue.GetUnits().Count());

            collection.Dispose();
            queue.Dispose();
            uc.Dispose();
        }

        [Test]
        public void IsServeAllPositionsWorking()
        {
            var unitsRepository = new Repository<Unit>();
            var time = new GameTime();
            var level = LevelDefinitionSetups.GetDefault();
            var random = new SessionRandom();
            var unitSettings = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            unitSettings.Speed = 1;
            unitSettings.SpeedUp = 1;
            unitSettings.SpeedUpDistance = 1;
            unitSettings.MinSpeed = 1f;
            unitSettings.RotationSpeed = 1f;
            IGameTime.Default = time;

            var uc = new UnitsService(unitsRepository, unitSettings, level, random);
            var crowd = new UnitsCrowdService(unitsRepository, uc, time, level, random);
            var queue = new UnitsCustomerQueueService(unitsRepository, uc, crowd, time, random, new GameVector3(0, 0, 1), 1);

            var collection = new ViewsCollection();
            new UnitsPresenter(unitsRepository, new UnitsManagerView(collection), unitSettings);

            queue.SetQueueSize(4);
            queue.ServeCustomer();
            time.MoveTime(2);

            Assert.AreEqual(4, queue.GetUnits().Count());

            queue.ServeAll();
            // 1
            Assert.AreEqual(3, queue.GetUnits().Count());
            Assert.AreEqual(new GameVector3(1, 0, 0), queue.GetUnits().ElementAt(0).Position);
            Assert.AreEqual(new GameVector3(2, 0, 0), queue.GetUnits().ElementAt(1).Position);
            Assert.AreEqual(new GameVector3(3, 0, 0), queue.GetUnits().ElementAt(2).Position);
            time.MoveTime(1f);
            // 2
            Assert.AreEqual(2, queue.GetUnits().Count());
            Assert.AreEqual(new GameVector3(2, 0, 0), queue.GetUnits().ElementAt(0).Position);
            Assert.AreEqual(new GameVector3(3, 0, 0), queue.GetUnits().ElementAt(1).Position);
            time.MoveTime(1f);
            // 3
            Assert.AreEqual(1, queue.GetUnits().Count());
            Assert.AreEqual(new GameVector3(3, 0, 0), queue.GetUnits().ElementAt(0).Position);
            time.MoveTime(1f);
            // 4
            Assert.AreEqual(0, queue.GetUnits().Count());
            collection.Dispose();
            queue.Dispose();
            uc.Dispose();
        }

        [Test]
        public void IsServeCoinsWorking()
        {
            var unitsRepository = new Repository<Unit>();
            var time = new GameTime();
            var level = LevelDefinitionSetups.GetDefault();
            var random = new SessionRandom();
            var unitSettings = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            IGameTime.Default = time;

            var uc = new UnitsService(unitsRepository, unitSettings, level, random);
            var crowd = new UnitsCrowdService(unitsRepository, uc, time, level, random);
            var queue = new UnitsCustomerQueueService(unitsRepository, uc, crowd, time, random, new GameVector3(0, 0, 1), 0);
            var coins = new CoinsService();

            var collection = new ViewsCollection();
            new UnitsPresenter(unitsRepository, new UnitsManagerView(collection), unitSettings);

            Assert.AreEqual(0, coins.Value);

            queue.SetQueueSize(2);
            queue.ServeCustomer(); // add new one
            queue.ServeCustomer(); // serve
            time.MoveTime(1);

            Assert.AreEqual(1, coins.Value);

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
                .UpdateDefinition<LevelDefinitionMock>(x => x.CrowdUnitsAmount = 0)
                .Build();

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.AreEqual(1, IBattleLevel.Default.Resources.Points.TargetLevel);
            Assert.AreEqual(0, IBattleLevel.Default.Resources.Points.CurrentLevel);
            Assert.AreEqual(1, game.LevelCollection.FindViews<UnitView>().Count);

            game.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
