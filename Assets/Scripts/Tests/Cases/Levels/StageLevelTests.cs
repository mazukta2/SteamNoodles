using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels.Starters;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Tests.Environment;
using NUnit.Framework;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Common;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Services.Flow;
using Game.Assets.Scripts.Game.Logic.Services.Levels;
using Game.Assets.Scripts.Game.Logic.Services.Resources;
using Game.Assets.Scripts.Game.Logic.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Services.Session;
using Game.Assets.Scripts.Game.Logic.Services.Units;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Tests.Cases.Levels
{
    public class StageLevelTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void StageLevelServiceIsLoadedByScepifics()
        {
            var manager = new ServiceManager();
            manager.Add(new Database<ConstructionSchemeEntity>());
            manager.Add(new Database<UnitType>());
            manager.Add(new GameAssetsService(new AssetsMock()));
            manager.Add(new GameControlsService(new ControlsMock()));
            
            IModelServices.Default = manager;
            IPresenterServices.Default = manager;
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
            
            var schemes = new Database<ConstructionSchemeEntity>();
            var scheme = schemes.Add(new ConstructionSchemeEntity(construction, settings));

            var customer = new CustomerDefinition();
            var unitsDefinition = new UnitsSettingsDefinition();
            unitsDefinition.Speed = 1;
            unitsDefinition.MinSpeed = 1;

            var units = new Database<UnitType>();
            var unit = units.Add(new UnitType(customer, unitsDefinition));

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
            manager.Add(new Database<ConstructionSchemeEntity>());
            manager.Add(new Database<UnitType>());
            manager.Add(new GameControlsService(new ControlsMock()));

            IModelServices.Default = manager;
            var level = new StageLevel();
            var service = new StageLevelService(level, manager, new SessionRandom(), new GameTime());
            manager.Add(service);

            Assert.IsTrue(manager.Has<StageTurnService>());
            Assert.IsTrue(manager.Has<RewardsService>());
            Assert.IsTrue(manager.Has<HandService>());
            Assert.IsTrue(manager.Has<UnitsTypesService>());
            Assert.IsTrue(manager.Has<UnitsService>());
            Assert.IsTrue(manager.Has<UnitsCrowdService>());
            Assert.IsTrue(manager.Has<UnitsCustomerQueueService>());
            Assert.IsTrue(manager.Has<UnitsMovementsService>());
            Assert.IsTrue(manager.Has<BuildingPointsService>());
            // Assert.IsTrue(manager.Has<BuildingService>());
            Assert.IsTrue(manager.Has<SchemesService>());
            Assert.IsTrue(manager.Has<CoinsService>());

            Assert.IsTrue(manager.Has<Database<ConstructionCardEntity>>());
            Assert.IsTrue(manager.Has<Database<ConstructionEntity>>());
            Assert.IsTrue(manager.Has<Database<Unit>>());
            Assert.IsTrue(manager.Has<SingletonDatabase<Deck<ConstructionSchemeEntity>>>());
            Assert.IsTrue(manager.Has<SingletonDatabase<Deck<UnitType>>>());
            Assert.IsTrue(manager.Has<SingletonDatabase<FieldEntity>>());

            manager.Remove(service);

            Assert.IsFalse(manager.Has<StageTurnService>());
            Assert.IsFalse(manager.Has<RewardsService>());
            Assert.IsFalse(manager.Has<HandService>());
            Assert.IsFalse(manager.Has<UnitsTypesService>());
            Assert.IsFalse(manager.Has<UnitsService>());
            Assert.IsFalse(manager.Has<UnitsCrowdService>());
            Assert.IsFalse(manager.Has<UnitsCustomerQueueService>());
            Assert.IsFalse(manager.Has<UnitsMovementsService>());
            Assert.IsFalse(manager.Has<BuildingPointsService>());
            // Assert.IsFalse(manager.Has<BuildingService>());
            Assert.IsFalse(manager.Has<SchemesService>());
            Assert.IsFalse(manager.Has<CoinsService>());

            Assert.IsFalse(manager.Has<Database<ConstructionCardEntity>>());
            Assert.IsFalse(manager.Has<Database<ConstructionEntity>>());
            Assert.IsFalse(manager.Has<Database<Unit>>());
            Assert.IsFalse(manager.Has<SingletonDatabase<Deck<ConstructionSchemeEntity>>>());
            Assert.IsFalse(manager.Has<SingletonDatabase<Deck<UnitType>>>());
            Assert.IsFalse(manager.Has<SingletonDatabase<FieldEntity>>());

            manager.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
