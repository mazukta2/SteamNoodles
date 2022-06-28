﻿using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using NUnit.Framework;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Services.Common;
using Game.Assets.Scripts.Game.Logic.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Flow;
using Game.Assets.Scripts.Game.Logic.Services.Resources;
using Game.Assets.Scripts.Game.Logic.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Services.Session;
using Game.Assets.Scripts.Game.Logic.Services.Units;
using Game.Assets.Scripts.Game.Logic.Services.Units.QueueAnimations;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Units;
using static Game.Assets.Scripts.Game.Logic.Entities.Units.UnitEntity;

namespace Game.Assets.Scripts.Tests.Cases.Game.Customers
{
    public class CustomersQueueTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void PointsConvertsToQueueSize()
        {
            var units = new Database<UnitEntity>();
            var unitTypes = new Database<UnitType>();
            var deck = new DeckService<UnitType>();
            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var time = new GameTime();
            var random = new SessionRandom();

            var unitTypesService = new UnitsTypesService(unitTypes, deck);

            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(0,0, 1, 1));

            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var coins = new CoinsService();
            var customers = new UnitsCustomerQueueService(units, unitsService, crowd, coins, points, time, random);

            Assert.AreEqual(new BuildingLevel(0), points.GetCurrentLevel());
            Assert.AreEqual(new QueueSize(0), customers.GetQueueSize());

            points.Change(new BuildingPoints(9));
            Assert.AreEqual(new BuildingLevel(2), points.GetCurrentLevel());

            Assert.AreEqual(new QueueSize(2), customers.GetQueueSize());

            unitsService.Dispose();
            crowd.Dispose();
            points.Dispose();
            customers.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void QueueSpawnsUnits()
        {
            var units = new Database<UnitEntity>();
            var unitTypes = new Database<UnitType>();
            var deck = new DeckService<UnitType>();
            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var time = new GameTime();
            var random = new SessionRandom();

            var unitTypesService = new UnitsTypesService(unitTypes, deck);

            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(0, 0, 1, 1));

            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var coins = new CoinsService();
            var custumers = new UnitsCustomerQueueService(units, unitsService, crowd, coins, points, time, random);
            Assert.AreEqual(0, units.Count);

            custumers.SetQueueSize(new QueueSize(1));

            Assert.AreEqual(1, units.Count);
            Assert.AreEqual(BehaviourState.InQueue, units.Get().First().State);

            unitsService.Dispose();
            crowd.Dispose();
            points.Dispose();
            custumers.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void QueuePositioningIsAlightWithFirstBuilding()
        {
            var units = new Database<UnitEntity>();
            var unitTypes = new Database<UnitType>();
            var deck = new DeckService<UnitType>();
            var constructionsRepository = new Database<ConstructionEntity>();
            var constructionsCardsRepository = new Database<ConstructionCardEntity>();
            var constructionsSchemeRepository = new Database<ConstructionSchemeEntity>();

            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var time = new GameTime();
            var random = new SessionRandom();

            var unitTypesService = new UnitsTypesService(unitTypes, deck);

            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(0, 0, 1, 1));

            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var coins = new CoinsService();
            var customers = new UnitsCustomerQueueService(units, unitsService, crowd, coins, points, time, random);

            var handService = new HandService(constructionsCardsRepository);
            var field = new FieldEntity(1, new IntPoint(11, 11));
            // var buildingService = new BuildingService(constructionsRepository);

            var turnService = new StageTurnService(constructionsRepository, customers);

            var scheme = new ConstructionSchemeEntity();
            constructionsSchemeRepository.Add(scheme);
            var card = new ConstructionCardEntity(scheme);
            constructionsCardsRepository.Add(card);
            Assert.AreEqual(0, constructionsRepository.Count);

            // var construction = buildingService.Build(card, new FieldPosition(field, 1, 1), new FieldRotation());
            // var constructionPosition = construction.GetWorldPosition();

            // Assert.AreEqual(constructionPosition.X, customers.GetQueuePosition().X);

            unitsService.Dispose();
            crowd.Dispose();
            points.Dispose();
            customers.Dispose();
            turnService.Dispose();
            points.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void MoveUnitsToPositionsInQueueWorks()
        {
            var time = new GameTime();
            var random = new SessionRandom();

            var units = new Database<UnitEntity>();
            var unitTypes = new Database<UnitType>();
            var deck = new DeckService<UnitType>();

            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var unitTypesService = new UnitsTypesService(unitTypes, deck);
            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(-10, -10, 10, 10));
            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var coins = new CoinsService();
            var queue = new UnitsCustomerQueueService(units, unitsService, crowd, coins, points, time, random);
            var movement = new UnitsMovementsService(units, time);

            var moveUnits = new MoveUnitsToPositionsInQueue(units, unitsService, queue);

            var unit1 = new UnitEntity(new GameVector3(1, 0, 0), new GameVector3(1, 0, 0), type, random);
            var unit2 = new UnitEntity(new GameVector3(2, 0, 0), new GameVector3(2, 0, 0), type, random);

            unit1.SetBehaviourState(BehaviourState.InQueue);
            unit2.SetBehaviourState(BehaviourState.InQueue);

            units.Add(unit1);
            units.Add(unit2);

            Assert.AreEqual(new GameVector3(0, 0, 0), queue.GetPositionFor(0));
            Assert.AreEqual(new GameVector3(1, 0, 0), queue.GetPositionFor(1));

            moveUnits.Play();

            Assert.AreEqual(new GameVector3(0, 0, 0), units.Get(unit1.Id).Target);
            Assert.AreEqual(new GameVector3(1, 0, 0), units.Get(unit2.Id).Target);

            time.MoveTime(100);

            Assert.AreEqual(new GameVector3(0, 0, 0), units.Get(unit1.Id).Position);
            Assert.AreEqual(new GameVector3(1, 0, 0), units.Get(unit2.Id).Position);

            unitsService.Dispose();
            crowd.Dispose();
            points.Dispose();
            queue.Dispose();
            points.Dispose();
            movement.Dispose();
            moveUnits.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void ServeFirstCustomerWorks()
        {
            var time = new GameTime();
            var random = new SessionRandom();

            var units = new Database<UnitEntity>();
            var unitTypes = new Database<UnitType>();
            var deck = new DeckService<UnitType>();

            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var unitTypesService = new UnitsTypesService(unitTypes, deck);
            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(-10, -10, 10, 10));
            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var coins = new CoinsService();
            var queue = new UnitsCustomerQueueService(units, unitsService, crowd, coins, points, time, random);
            var movement = new UnitsMovementsService(units, time);

            var serveFirstUnit = new ServeFirstCustomer(queue, crowd, Serve);

            var unit1 = new UnitEntity(new GameVector3(1, 0, 0), new GameVector3(1, 0, 0), type, random);
            var unit2 = new UnitEntity(new GameVector3(2, 0, 0), new GameVector3(2, 0, 0), type, random);
            unit1.SetBehaviourState(BehaviourState.InQueue);
            unit2.SetBehaviourState(BehaviourState.InQueue);
            units.Add(unit1);
            units.Add(unit2);

            var isServed = false;

            Assert.AreEqual(BehaviourState.InQueue, units.Get(unit1.Id).State);
            Assert.AreEqual(BehaviourState.InQueue, units.Get(unit2.Id).State);

            serveFirstUnit.Play();

            Assert.IsTrue(isServed);
            Assert.AreEqual(BehaviourState.InCrowd, units.Get(unit1.Id).State);
            Assert.AreEqual(BehaviourState.InQueue, units.Get(unit2.Id).State);

            unitsService.Dispose();
            crowd.Dispose();
            points.Dispose();
            queue.Dispose();
            points.Dispose();
            movement.Dispose();
            serveFirstUnit.Dispose();

            void Serve(UnitEntity obj)
            {
                isServed = true;
            }
        }

        [Test, Order(TestCore.ModelOrder)]
        public void ServingReleaseUnitToTheCrowd()
        {
            var time = new GameTime();
            var random = new SessionRandom();

            var units = new Database<UnitEntity>();
            var unitTypes = new Database<UnitType>();
            var deck = new DeckService<UnitType>();

            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var unitTypesService = new UnitsTypesService(unitTypes, deck);
            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(-10, -10, 10, 10));
            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var coins = new CoinsService();
            var queue = new UnitsCustomerQueueService(units, unitsService, crowd, coins, points, time, random);
            var movement = new UnitsMovementsService(units, time);

            Assert.AreEqual(0, crowd.GetUnits().Count);
            Assert.AreEqual(0, queue.GetQueueUnits().Count);
            Assert.AreEqual(0, units.Count);

            queue.SetQueueSize(new QueueSize(1));
            time.MoveTime(100);
            Assert.IsFalse(queue.IsAnimating());

            Assert.AreEqual(0, crowd.GetUnits().Count);
            Assert.AreEqual(1, queue.GetQueueUnits().Count);
            Assert.AreEqual(1, units.Count);

            queue.TurnQueue();

            Assert.AreEqual(1, crowd.GetUnits().Count);
            Assert.AreEqual(1, queue.GetQueueUnits().Count);
            Assert.AreEqual(2, units.Count);

            unitsService.Dispose();
            crowd.Dispose();
            points.Dispose();
            queue.Dispose();
            points.Dispose();
            movement.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void LevelCrowdGetUnitsCorrectly()
        {
            var unitTypes = new Database<UnitType>();
            var units = new Database<UnitEntity>();

            var time = new GameTime();
            var random = new SessionRandom();

            var deck = new DeckService<UnitType>();

            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var unitTypesService = new UnitsTypesService(unitTypes, deck);
            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(-10, -4, 20, 8));
            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var coins = new CoinsService();
            var queue = new UnitsCustomerQueueService(units, unitsService, crowd, coins, points, time, random);
            var movement = new UnitsMovementsService(units, time);

            var unit = unitsService.SpawnUnit(new GameVector3(0, 0, 0));
            crowd.SendToCrowd(unit, UnitsCrowdService.CrowdDirection.Left);

            Assert.AreEqual(new GameVector3(0, 0, 0), unit.Position);

            // right target
            Assert.AreEqual(-10, unit.Target.X);
            Assert.AreEqual(0, unit.Target.Y);
            Assert.GreaterOrEqual(unit.Target.Z, -4);
            Assert.LessOrEqual(unit.Target.Z, 4);

            // destoyed
            Assert.IsNotNull(units.Get(unit.Id));
            time.MoveTime(1);
            Assert.IsNotNull(units.Get(unit.Id));
            time.MoveTime(100);
            Assert.IsNull(units.Get(unit.Id));
            //

            // same in oposite direction
            var unit2 = unitsService.SpawnUnit(new GameVector3(0, 0, 0));
            crowd.SendToCrowd(unit2, UnitsCrowdService.CrowdDirection.Right);

            Assert.AreEqual(new GameVector3(0, 0, 0), unit2.Position);
            Assert.AreEqual(10, unit2.Target.X);
            Assert.AreEqual(0, unit2.Target.Y);
            Assert.GreaterOrEqual(unit2.Target.Z, -4);
            Assert.LessOrEqual(unit2.Target.Z, 4);

            Assert.IsNotNull(units.Get(unit2.Id));
            time.MoveTime(1);
            Assert.IsNotNull(units.Get(unit2.Id));
            time.MoveTime(100);
            Assert.IsNull(units.Get(unit2.Id));

            unitsService.Dispose();
            crowd.Dispose();
            points.Dispose();
            queue.Dispose();
            points.Dispose();
            movement.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void QueueFreeAllWorking()
        {
            var unitTypes = new Database<UnitType>();
            var units = new Database<UnitEntity>();

            var time = new GameTime();
            var random = new SessionRandom();

            var deck = new DeckService<UnitType>();

            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var unitTypesService = new UnitsTypesService(unitTypes, deck);
            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(-10, -4, 20, 8));
            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var coins = new CoinsService();
            var queue = new UnitsCustomerQueueService(units, unitsService, crowd, coins, points, time, random);
            var movement = new UnitsMovementsService(units, time);

            queue.SetQueueSize(new QueueSize(2));
            time.MoveTime(100);

            Assert.AreEqual(2, queue.GetUnits().Count());
            Assert.AreEqual(0, crowd.GetUnits().Count);

            queue.FreeAll();

            Assert.AreEqual(0, queue.GetUnits().Count());
            Assert.AreEqual(2, crowd.GetUnits().Count);

            queue.TurnQueue();
            time.MoveTime(1);

            Assert.AreEqual(2, queue.GetUnits().Count());
            Assert.AreEqual(2, crowd.GetUnits().Count);

            unitsService.Dispose();
            crowd.Dispose();
            points.Dispose();
            queue.Dispose();
            points.Dispose();
            movement.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void ServeAllWorking()
        {
            var unitTypes = new Database<UnitType>();
            var units = new Database<UnitEntity>();

            var time = new GameTime();
            var random = new SessionRandom();

            var deck = new DeckService<UnitType>();

            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var unitTypesService = new UnitsTypesService(unitTypes, deck);
            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(-10, -4, 20, 8));
            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var coins = new CoinsService();
            var queue = new UnitsCustomerQueueService(units, unitsService, crowd, coins, points, time, random);
            var movement = new UnitsMovementsService(units, time);

            queue.SetQueueSize(new QueueSize(2));
            time.MoveTime(100);

            var unitList = queue.GetUnits().ToArray();
            Assert.AreEqual(2, unitList.Count());
            Assert.AreEqual(0, crowd.GetUnits().Count);

            queue.ServeAll();
            time.MoveTime(1);

            Assert.AreEqual(2, queue.GetUnits().Count());
            Assert.AreEqual(2, crowd.GetUnits().Count);

            Assert.AreEqual(units.Get().ElementAt(0).Id, unitList[0].Id);
            Assert.AreEqual(units.Get().ElementAt(1).Id, unitList[1].Id);
            Assert.AreEqual(units.Get().ElementAt(2).Id, queue.GetUnits().ElementAt(0).Id);
            Assert.AreEqual(units.Get().ElementAt(3).Id, queue.GetUnits().ElementAt(1).Id);

            queue.TurnQueue();
            time.MoveTime(1);

            Assert.AreEqual(2, queue.GetUnits().Count());
            Assert.AreEqual(3, crowd.GetUnits().Count);

            unitsService.Dispose();
            crowd.Dispose();
            points.Dispose();
            queue.Dispose();
            points.Dispose();
            movement.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void ServeAllTimerWorking()
        {
            var unitTypes = new Database<UnitType>();
            var units = new Database<UnitEntity>();

            var time = new GameTime();
            var random = new SessionRandom();

            var deck = new DeckService<UnitType>();

            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var unitTypesService = new UnitsTypesService(unitTypes, deck);
            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(-10, -4, 20, 8));
            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var coins = new CoinsService();
            var queue = new UnitsCustomerQueueService(units, unitsService, crowd, coins, points, time, random, new GameVector3(0, 0, 1), 0, 1);
            var movement = new UnitsMovementsService(units, time);

            queue.SetQueueSize(new QueueSize(2));
            time.MoveTime(100);

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

            unitsService.Dispose();
            crowd.Dispose();
            points.Dispose();
            queue.Dispose();
            points.Dispose();
            movement.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void ServeAllPositionsWorking()
        {
            var unitTypes = new Database<UnitType>();
            var units = new Database<UnitEntity>();

            var time = new GameTime();
            var random = new SessionRandom();

            var deck = new DeckService<UnitType>();

            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var unitTypesService = new UnitsTypesService(unitTypes, deck);
            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(-10, -4, 20, 8));
            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var coins = new CoinsService();
            var queue = new UnitsCustomerQueueService(units, unitsService, crowd, coins, points, time, random, new GameVector3(0, 0, 1), 0, 1);
            var movement = new UnitsMovementsService(units, time);

            queue.SetQueueSize(new QueueSize(4));
            time.MoveTime(100);

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

            unitsService.Dispose();
            crowd.Dispose();
            points.Dispose();
            queue.Dispose();
            points.Dispose();
            movement.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void ServeCoinsWorking()
        {
            var unitTypes = new Database<UnitType>();
            var units = new Database<UnitEntity>();

            var time = new GameTime();
            var random = new SessionRandom();

            var deck = new DeckService<UnitType>();

            var type = new UnitType(coins: 1);
            unitTypes.Add(type);
            deck.Add(type);

            var unitTypesService = new UnitsTypesService(unitTypes, deck);
            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(-10, -4, 20, 8));
            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var coins = new CoinsService();
            var queue = new UnitsCustomerQueueService(units, unitsService, crowd, coins, points, time, random, new GameVector3(0, 0, 1));
            var movement = new UnitsMovementsService(units, time);

            Assert.AreEqual(0, coins.Value);

            queue.SetQueueSize(new QueueSize(2));
            time.MoveTime(100);
            queue.TurnQueue(); // serve
            time.MoveTime(1);

            Assert.AreEqual(1, coins.Value);

            unitsService.Dispose();
            crowd.Dispose();
            points.Dispose();
            queue.Dispose();
            points.Dispose();
            movement.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void FirstUnitAppearsAfterFirstBuilding()
        {
            var units = new Database<UnitEntity>();
            var unitTypes = new Database<UnitType>();
            var deck = new DeckService<UnitType>();
            var constructionsRepository = new Database<ConstructionEntity>();
            var constructionsCardsRepository = new Database<ConstructionCardEntity>();
            var constructionsSchemeRepository = new Database<ConstructionSchemeEntity>();

            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var time = new GameTime();
            var random = new SessionRandom();

            var unitTypesService = new UnitsTypesService(unitTypes, deck);

            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(0, 0, 1, 1));

            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var coins = new CoinsService();
            var customers = new UnitsCustomerQueueService(units, unitsService, crowd, coins, points, time, random);

            var handService = new HandService(constructionsCardsRepository);
            var field = new FieldEntity(1, new IntPoint(11, 11));
            // var buildingService = new BuildingService(constructionsRepository);
            var pointsOnBuilding = new PointsOnBuildingService(constructionsRepository, points);

            var turnService = new StageTurnService(constructionsRepository, customers);

            var scheme = ConstructionSchemeEntity.DefaultWithPoints(new BuildingPoints(3));
            constructionsSchemeRepository.Add(scheme);
            var card = new ConstructionCardEntity(scheme);
            constructionsCardsRepository.Add(card);
            Assert.AreEqual(0, constructionsRepository.Count);

            // var construction = buildingService.Build(card, new FieldPosition(field, 1, 1), new FieldRotation());

            Assert.AreEqual(new BuildingLevel(1), points.GetTargetLevel());
            Assert.AreEqual(new BuildingLevel(1), points.GetCurrentLevel());
            Assert.AreEqual(1, units.Get().Count);

            unitsService.Dispose();
            crowd.Dispose();
            points.Dispose();
            customers.Dispose();
            turnService.Dispose();
            points.Dispose();
            pointsOnBuilding.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
