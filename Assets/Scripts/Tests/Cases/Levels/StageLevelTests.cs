using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels.Starters;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Common;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Services;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Flow;
using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Tests.Environment;
using Game.Tests.Cases;
using NUnit.Framework;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Cases.Levels
{
    public class StageLevelTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void StageLevelServiceIsLoadedByScepifics()
        {
            var manager = new ServiceManager();
            manager.Add(new Repository<ConstructionScheme>());
            manager.Add(new Repository<UnitType>());
            IModelServices.Default = manager;
            IGameRandom.Default = new SessionRandom();
            IGameTime.Default = new GameTime();

            var level = new StageLevel();
            var starter = new StageLevelSpecifics();

            starter.StartServices(level);

            Assert.IsTrue(manager.Has<StageLevelService>());
        }


        [Test, Order(TestCore.ModelOrder)]
        public void StageLevelFilledByDefinitionsCorrectly()
        {
            var construction = new ConstructionDefinition();
            construction.Name = "construction";
            var settings = new ConstructionsSettingsDefinition();
            
            var schemes = new Repository<ConstructionScheme>();
            var scheme = schemes.Add(new ConstructionScheme(construction, settings)).Get();

            var customer = new CustomerDefinition();
            var unitsDefinition = new UnitsSettingsDefinition();
            unitsDefinition.Speed = 1;
            unitsDefinition.MinSpeed = 1;

            var units = new Repository<UnitType>();
            var unit = units.Add(new UnitType(customer, unitsDefinition)).Get();

            var constructionsDefinitions = new ConstructionsSettingsDefinition();
            constructionsDefinitions.CellSize = 1;
            constructionsDefinitions.PieceMovingTime = 1;
            constructionsDefinitions.PieceSpawningTime = 1;
            constructionsDefinitions.LevelUpOffset = 1;
            constructionsDefinitions.LevelUpPower = 1;

            var levelDefinition = new LevelDefinition();
            levelDefinition.DefId = new DefId("name");
            levelDefinition.SceneName = "name";
            levelDefinition.PlacementField = new PlacementFieldDefinition() { Size = new IntPoint(1, 1) };
            levelDefinition.StartingHand = new List<ConstructionDefinition>() { construction, construction, construction };
            levelDefinition.ConstructionsReward = new Dictionary<ConstructionDefinition, int>() { { construction, 1 } };
            levelDefinition.BaseCrowdUnits = new Dictionary<CustomerDefinition, int>() { { customer, 1 } };
            levelDefinition.UnitsRect = new FloatRect(1, 1, 1, 1);
            levelDefinition.CrowdUnitsAmount = 1;

            var level = new StageLevel(levelDefinition, constructionsDefinitions, schemes, units);

            Assert.AreEqual("name", level.SceneName);
            Assert.AreEqual(3, level.StartingSchemes.Count);
            Assert.AreEqual(new IntPoint(1, 1), level.PlacementFieldSize);
            Assert.AreEqual(1, level.ConstructionsReward.Count);
            Assert.AreEqual(1, level.CrowdUnits.Count);
            Assert.AreEqual(new FloatRect(1, 1, 1, 1), level.UnitsRect);
            Assert.AreEqual(1, level.CrowdUnitsAmount);
            Assert.AreEqual(1, level.CellSize);
            Assert.AreEqual(1, level.PieceSpawningTime);
            Assert.AreEqual(1, level.PieceMovingTime);
            Assert.AreEqual(1, level.LevelUpPower);
            Assert.AreEqual(1, level.LevelUpOffset);
        }

        [Test, Order(TestCore.ModelOrder)]
        public void StageLevelServiceStartAllNeededServices()
        {
            var manager = new ServiceManager();
            manager.Add(new Repository<ConstructionScheme>());
            manager.Add(new Repository<UnitType>());

            IModelServices.Default = manager;
            IGameAssets.Default = new GameAssets(new AssetsMock());
            var level = new StageLevel();
            var service = new StageLevelService(level, manager, new SessionRandom(), new GameTime());
            manager.Add(service);

            Assert.IsTrue(manager.Has<StageTurnService>());
            Assert.IsTrue(manager.Has<RewardsService>());
            Assert.IsTrue(manager.Has<ConstructionsService>());
            Assert.IsTrue(manager.Has<HandService>());
            Assert.IsTrue(manager.Has<UnitsTypesService>());
            Assert.IsTrue(manager.Has<UnitsService>());
            Assert.IsTrue(manager.Has<UnitsCrowdService>());
            Assert.IsTrue(manager.Has<UnitsCustomerQueueService>());
            Assert.IsTrue(manager.Has<UnitsMovementsService>());
            Assert.IsTrue(manager.Has<BuildingPointsService>());
            Assert.IsTrue(manager.Has<FieldService>());
            Assert.IsTrue(manager.Has<BuildingService>());
            Assert.IsTrue(manager.Has<SchemesService>());
            Assert.IsTrue(manager.Has<CoinsService>());
            Assert.IsTrue(manager.Has<BuildingModeService>());

            Assert.IsTrue(manager.Has<FieldRequestsService>()); 

            Assert.IsTrue(manager.Has<Repository<ConstructionCard>>());
            Assert.IsTrue(manager.Has<Repository<Construction>>());
            Assert.IsTrue(manager.Has<Repository<Unit>>());
            Assert.IsTrue(manager.Has<SingletonRepository<Deck<ConstructionScheme>>>());
            Assert.IsTrue(manager.Has<SingletonRepository<Deck<UnitType>>>());

            manager.Remove(service);

            Assert.IsFalse(manager.Has<StageTurnService>());
            Assert.IsFalse(manager.Has<RewardsService>());
            Assert.IsFalse(manager.Has<ConstructionsService>());
            Assert.IsFalse(manager.Has<HandService>());
            Assert.IsFalse(manager.Has<UnitsTypesService>());
            Assert.IsFalse(manager.Has<UnitsService>());
            Assert.IsFalse(manager.Has<UnitsCrowdService>());
            Assert.IsFalse(manager.Has<UnitsCustomerQueueService>());
            Assert.IsFalse(manager.Has<UnitsMovementsService>());
            Assert.IsFalse(manager.Has<BuildingPointsService>());
            Assert.IsFalse(manager.Has<FieldService>());
            Assert.IsFalse(manager.Has<BuildingService>());
            Assert.IsFalse(manager.Has<SchemesService>());
            Assert.IsFalse(manager.Has<CoinsService>());
            Assert.IsFalse(manager.Has<BuildingModeService>());

            Assert.IsFalse(manager.Has<FieldRequestsService>());

            Assert.IsFalse(manager.Has<Repository<ConstructionCard>>());
            Assert.IsFalse(manager.Has<Repository<Construction>>());
            Assert.IsFalse(manager.Has<Repository<Unit>>());
            Assert.IsFalse(manager.Has<SingletonRepository<Deck<ConstructionScheme>>>());
            Assert.IsFalse(manager.Has<SingletonRepository<Deck<UnitType>>>());

            manager.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
