using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Services.Common;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Flow;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units.QueueAnimations;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using Game.Tests.Cases;
using NUnit.Framework;
using System;
using System.Linq;
using static Game.Assets.Scripts.Game.Logic.Models.Entities.Units.Unit;

namespace Game.Assets.Scripts.Tests.Cases.Game.Customers
{
    public class CustomersQueueTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void IsPointsConvertsToQueueSize()
        {
            var units = new Repository<Unit>();
            var unitTypes = new Repository<UnitType>();
            var deck = new DeckService<UnitType>();
            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var time = new GameTime();
            var random = new SessionRandom();

            var unitTypesService = new UnitsTypesService(unitTypes, deck);

            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(0,0, 1, 1), 0);

            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var custumers = new UnitsCustomerQueueService(units, unitsService, crowd, points, time, random);

            Assert.AreEqual(new BuildingLevel(0), points.GetCurrentLevel());
            Assert.AreEqual(new QueueSize(0), custumers.GetQueueSize());

            points.Change(new BuildingPoints(9));
            Assert.AreEqual(new BuildingLevel(2), points.GetCurrentLevel());

            Assert.AreEqual(new QueueSize(2), custumers.GetQueueSize());

            unitsService.Dispose();
            crowd.Dispose();
            points.Dispose();
            custumers.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsQueueSpawnsUnits()
        {
            var units = new Repository<Unit>();
            var unitTypes = new Repository<UnitType>();
            var deck = new DeckService<UnitType>();
            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var time = new GameTime();
            var random = new SessionRandom();

            var unitTypesService = new UnitsTypesService(unitTypes, deck);

            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(0, 0, 1, 1), 0);

            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var custumers = new UnitsCustomerQueueService(units, unitsService, crowd, points, time, random);
            Assert.AreEqual(0, units.Count);

            custumers.SetQueueSize(new QueueSize(1));

            Assert.AreEqual(1, units.Count);
            Assert.AreEqual(BehaviourState.InQueue, units.GetAll().First().State);

            unitsService.Dispose();
            crowd.Dispose();
            points.Dispose();
            custumers.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsQueuePositioningIsAlightWithFirstBuilding()
        {
            var units = new Repository<Unit>();
            var unitTypes = new Repository<UnitType>();
            var deck = new DeckService<UnitType>();
            var constructionsRepository = new Repository<Construction>();
            var constructionsCardsRepository = new Repository<ConstructionCard>();
            var constructionsSchemeRepository = new Repository<ConstructionScheme>();

            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var time = new GameTime();
            var random = new SessionRandom();

            var unitTypesService = new UnitsTypesService(unitTypes, deck);

            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(0, 0, 1, 1), 0);

            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var custumers = new UnitsCustomerQueueService(units, unitsService, crowd, points, time, random);

            var handService = new HandService(constructionsCardsRepository, constructionsSchemeRepository);
            var fieldService = new FieldService(1, new IntPoint(11, 11));
            var constructionsService = new ConstructionsService(constructionsRepository, fieldService);
            var buildngService = new BuildingService(constructionsRepository, constructionsService, points, handService, fieldService);

            var turnService = new StageTurnService(constructionsRepository, fieldService, buildngService, custumers);

            var scheme = new ConstructionScheme();
            constructionsSchemeRepository.Add(scheme);
            var card = new ConstructionCard(scheme);
            constructionsCardsRepository.Add(card);
            Assert.AreEqual(0, constructionsRepository.Count);

            var construction = buildngService.Build(card, new FieldPosition(1, 1), new FieldRotation());
            var constructionPosition = fieldService.GetWorldPosition(construction);

            Assert.AreEqual(constructionPosition.X, custumers.GetQueuePosition().X);

            unitsService.Dispose();
            crowd.Dispose();
            points.Dispose();
            custumers.Dispose();
            turnService.Dispose();
            points.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsMoveUnitsToPositionsInQueueWorks()
        {
            var time = new GameTime();
            var random = new SessionRandom();

            var units = new Repository<Unit>();
            var unitTypes = new Repository<UnitType>();
            var deck = new DeckService<UnitType>();

            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var unitTypesService = new UnitsTypesService(unitTypes, deck);
            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(-10, -10, 10, 10), 0);
            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var queue = new UnitsCustomerQueueService(units, unitsService, crowd, points, time, random);
            var movement = new UnitsMovementsService(units, time);

            var moveUnits = new MoveUnitsToPositionsInQueue(units, unitsService, queue);

            var unit1 = new Unit(new GameVector3(1, 0, 0), new GameVector3(1, 0, 0), type, random);
            var unit2 = new Unit(new GameVector3(2, 0, 0), new GameVector3(2, 0, 0), type, random);

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
        public void IsServeFirstCustomerWorks()
        {
            var time = new GameTime();
            var random = new SessionRandom();

            var units = new Repository<Unit>();
            var unitTypes = new Repository<UnitType>();
            var deck = new DeckService<UnitType>();

            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var unitTypesService = new UnitsTypesService(unitTypes, deck);
            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(-10, -10, 10, 10), 0);
            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var queue = new UnitsCustomerQueueService(units, unitsService, crowd, points, time, random);
            var movement = new UnitsMovementsService(units, time);

            var serveFirstUnit = new ServeFirstCustomer(queue, crowd, Serve);

            var unit1 = new Unit(new GameVector3(1, 0, 0), new GameVector3(1, 0, 0), type, random);
            var unit2 = new Unit(new GameVector3(2, 0, 0), new GameVector3(2, 0, 0), type, random);
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

            void Serve(Unit obj)
            {
                isServed = true;
            }
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsServingReleaseUnitToTheCrowd()
        {
            var time = new GameTime();
            var random = new SessionRandom();

            var units = new Repository<Unit>();
            var unitTypes = new Repository<UnitType>();
            var deck = new DeckService<UnitType>();

            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var unitTypesService = new UnitsTypesService(unitTypes, deck);
            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(-10, -10, 10, 10), 0);
            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var queue = new UnitsCustomerQueueService(units, unitsService, crowd, points, time, random);
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

        [Test]
        public void IsQueueVisualRight()
        {
            throw new System.Exception();
            //var unitsRepository = new Repository<Unit>();
            //var time = new GameTime();
            //var level = LevelDefinitionSetups.GetDefault();
            //var random = new SessionRandom();
            //var unitSettings = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            //unitSettings.Speed = 1;
            //unitSettings.SpeedUp = 1;
            //unitSettings.SpeedUpDistance = 1;
            //unitSettings.MinSpeed = 0.5f;
            //unitSettings.RotationSpeed = 0.5f;
            
            //var uc = new UnitsService(unitsRepository, unitSettings, level, random);

            //IGameTime.Default = time;
            //var crowd = new UnitsCrowdService(unitsRepository, uc, time, level, random);
            //var queue = new UnitsCustomerQueueService(unitsRepository, uc, crowd, time, random, new GameVector3(0, 0, 1));

            //var collection = new ViewsCollection();
            //new UnitsPresenter(unitsRepository, new UnitsManagerView(collection), unitSettings);

            //queue.SetQueueSize(2);
            //queue.ServeCustomer();

            //Assert.AreEqual(new GameVector3(1, 0, 0), queue.GetUnits().ToArray()[0].Position);
            //Assert.AreEqual(new GameVector3(2, 0, 0), queue.GetUnits().ToArray()[1].Position);

            //var views = collection.FindViews<UnitView>().ToList();
            //var originalRotation = views[0].Rotator.Rotation;
            //Assert.AreEqual(new GameVector3(-1, 0, 0), views[0].Rotator.Rotation.ToVector().GetRound());
            //Assert.AreEqual(new GameVector3(-1, 0, 0), views[1].Rotator.Rotation.ToVector().GetRound());

            //var timePassed = 0.1f;
            //time.MoveTime(timePassed);
            
            //var firstUnitTraget = queue.GetQueueFirstPosition() + queue.GetQueueFirstPositionOffset();
            //Assert.AreEqual(firstUnitTraget, queue.GetUnits().ToArray()[0].Target);
            //Assert.AreEqual(0.6f, queue.GetUnits().ToArray()[0].CurrentSpeed);
            //var targetRotation = (firstUnitTraget - new GameVector3(1, 0, 0)).ToQuaternion();
            
            //Assert.AreEqual(new GameVector3(1, 0, 0).MoveTowards(firstUnitTraget, 0.6f * timePassed), queue.GetUnits().ToArray()[0].Position);
            //Assert.AreEqual(new GameVector3(1.95f, 0, 0), queue.GetUnits().ToArray()[1].Position);
            //AssertHelpers.CompareVectors(GameQuaternion.RotateTowards(originalRotation, targetRotation, 0.5f * timePassed).ToVector(), 
            //    views[0].Rotator.Rotation.ToVector(), 0.01f);
            //Assert.AreEqual(new GameVector3(-1, 0, 0), views[1].Rotator.Rotation.ToVector().GetRound());

            //collection.Dispose();
            //queue.Dispose();
            //uc.Dispose();
        }

        [Test]
        public void IsLevelCrowdGetUnitsCorrectly()
        {
            throw new System.Exception();
            //var unitsRepository = new Repository<Unit>();
            //var time = new GameTime();
            //var level = LevelDefinitionSetups.GetDefault();
            //level.UnitsRect = new FloatRect(-10, -4, 20, 8);
            //level.CrowdUnitsAmount = 0;
            //var random = new SessionRandom();
            //var uc = new UnitsService(unitsRepository, UnitDefinitionSetup.GetDefaultUnitsDefinitions(), level, random);

            //var crowdRepository = new Repository<Unit>();
            //var crowd = new UnitsCrowdService(crowdRepository, uc, time, level, random);

            //var unit = uc.SpawnUnit(new GameVector3(0, 0, 0));
            //crowd.SendToCrowd(unit, UnitsCrowdService.CrowdDirection.Left);
            //Assert.AreEqual(new GameVector3(0, 0, 0), unit.Position);
            //Assert.AreEqual(-10, unit.Target.X);
            //Assert.AreEqual(0, unit.Target.Y);
            //Assert.GreaterOrEqual(unit.Target.Z, - 4);
            //Assert.LessOrEqual(unit.Target.Z, 4);
            //Assert.IsNotNull(unitsRepository.Get(unit.Id));
            //time.MoveTime(1);
            //Assert.IsNotNull(unitsRepository.Get(unit.Id));
            //time.MoveTime(100);
            //Assert.AreEqual(unit.Target, unit.Position);
            //Assert.IsNull(unitsRepository.Get(unit.Id));

            //var unit2 = uc.SpawnUnit(new GameVector3(0, 0, 0));
            //crowd.SendToCrowd(unit2, UnitsCrowdService.CrowdDirection.Right);
            //Assert.AreEqual(new GameVector3(0, 0, 0), unit2.Position);
            //Assert.AreEqual(10, unit2.Target.X);
            //Assert.AreEqual(0, unit2.Target.Y);
            //Assert.GreaterOrEqual(unit2.Target.Z, -4);
            //Assert.LessOrEqual(unit2.Target.Z, 4);
            //Assert.IsNotNull(unitsRepository.Get(unit2.Id));
            //time.MoveTime(1);
            //Assert.IsNotNull(unitsRepository.Get(unit2.Id));
            //time.MoveTime(100);
            //Assert.AreEqual(unit2.Target, unit2.Position);
            //Assert.IsNull(unitsRepository.Get(unit2.Id));

            //crowd.Dispose();
            //uc.Dispose();
        }

        [Test]
        public void IsQueueFreeAllWorking()
        {
            throw new System.Exception();
            //var unitsRepository = new Repository<Unit>();
            //var time = new GameTime();
            //var level = LevelDefinitionSetups.GetDefault();
            //var random = new SessionRandom();
            //var unitSettings = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            //IGameTime.Default = time;

            //var uc = new UnitsService(unitsRepository, unitSettings, level, random);

            //var crowd = new UnitsCrowdService(unitsRepository, uc, time, level, random);
            //var queue = new UnitsCustomerQueueService(unitsRepository, uc, crowd, time, random, new GameVector3(0, 0, 1));

            //var collection = new ViewsCollection();
            //new UnitsPresenter(unitsRepository, new UnitsManagerView(collection), unitSettings);

            //queue.SetQueueSize(2);
            //queue.ServeCustomer();
            //time.MoveTime(1);

            //Assert.AreEqual(2, collection.FindViews<UnitView>().Count());
            //Assert.AreEqual(2, queue.GetUnits().Count());
            //Assert.AreEqual(0, crowd.GetUnits().Count);

            //queue.FreeAll();

            //Assert.AreEqual(2, collection.FindViews<UnitView>().Count());
            //Assert.AreEqual(0, queue.GetUnits().Count());
            //Assert.AreEqual(2, crowd.GetUnits().Count);

            //queue.SetQueueSize(2);
            //queue.ServeCustomer();
            //time.MoveTime(1);

            //Assert.AreEqual(4, collection.FindViews<UnitView>().Count());
            //Assert.AreEqual(2, queue.GetUnits().Count());
            //Assert.AreEqual(2, crowd.GetUnits().Count);

            //collection.Dispose();
            //queue.Dispose();
            //uc.Dispose();
        }

        [Test]
        public void IsServeAllWorking()
        {
            throw new System.Exception();
            //var unitsRepository = new Repository<Unit>();
            //var time = new GameTime();
            //var level = LevelDefinitionSetups.GetDefault();
            //var random = new SessionRandom();
            //var unitSettings = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            //IGameTime.Default = time;

            //var uc = new UnitsService(unitsRepository, unitSettings, level, random);
            //var crowd = new UnitsCrowdService(unitsRepository, uc, time, level, random);
            //var queue = new UnitsCustomerQueueService(unitsRepository, uc, crowd, time, random, new GameVector3(0, 0, 1));


            //var collection = new ViewsCollection();
            //new UnitsPresenter(unitsRepository, new UnitsManagerView(collection), unitSettings);

            //queue.SetQueueSize(2);
            //queue.ServeCustomer();
            //time.MoveTime(1);

            //var unitList = queue.GetUnits().ToArray();
            //Assert.AreEqual(2, collection.FindViews<UnitView>().Count());
            //Assert.AreEqual(2, unitList.Count());
            //Assert.AreEqual(0, crowd.GetUnits().Count);

            //queue.ServeAll();
            //time.MoveTime(1);

            //Assert.AreEqual(4, collection.FindViews<UnitView>().Count());
            //Assert.AreEqual(2, queue.GetUnits().Count());
            //Assert.AreEqual(2, crowd.GetUnits().Count);

            //Assert.AreEqual(unitsRepository.Get().ElementAt(0), unitList[0]);
            //Assert.AreEqual(unitsRepository.Get().ElementAt(1), unitList[1]);
            //Assert.AreEqual(unitsRepository.Get().ElementAt(2), queue.GetUnits().ElementAt(0));
            //Assert.AreEqual(unitsRepository.Get().ElementAt(3), queue.GetUnits().ElementAt(1));

            //queue.SetQueueSize(2);
            //queue.ServeCustomer();
            //time.MoveTime(1);

            //Assert.AreEqual(5, collection.FindViews<UnitView>().Count());
            //Assert.AreEqual(2, queue.GetUnits().Count());
            //Assert.AreEqual(3, crowd.GetUnits().Count);

            //collection.Dispose();
            //queue.Dispose();
            //uc.Dispose();
        }

        [Test]
        public void IsServeAllTimerWorking()
        {
            throw new System.Exception();
            //var unitsRepository = new Repository<Unit>();
            //var time = new GameTime();
            //var level = LevelDefinitionSetups.GetDefault();
            //var random = new SessionRandom();
            //var unitSettings = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            //IGameTime.Default = time;

            //var uc = new UnitsService(unitsRepository, unitSettings, level, random);
            //var crowd = new UnitsCrowdService(unitsRepository, uc, time, level, random);
            //var queue = new UnitsCustomerQueueService(unitsRepository, uc, crowd, time, random, new GameVector3(0, 0, 1), 1);

            //var collection = new ViewsCollection();
            //new UnitsPresenter(unitsRepository, new UnitsManagerView(collection), unitSettings);

            //queue.SetQueueSize(2);
            //queue.ServeCustomer();
            //time.MoveTime(1);

            //Assert.AreEqual(2, queue.GetUnits().Count());

            //queue.ServeAll();
            //// first unit is server instantly
            //Assert.AreEqual(1, queue.GetUnits().Count());
            //time.MoveTime(0.5f);
            //Assert.AreEqual(1, queue.GetUnits().Count());
            //time.MoveTime(0.5f);

            //// second unit - after first iteration
            //Assert.AreEqual(0, queue.GetUnits().Count());
            //time.MoveTime(0.5f);
            //Assert.AreEqual(0, queue.GetUnits().Count());
            //time.MoveTime(0.5f);

            //// swawn first unit
            //Assert.AreEqual(1, queue.GetUnits().Count());
            //time.MoveTime(0.5f);
            //Assert.AreEqual(1, queue.GetUnits().Count());
            //time.MoveTime(0.5f);

            //// swawn second unit
            //Assert.AreEqual(2, queue.GetUnits().Count());

            //collection.Dispose();
            //queue.Dispose();
            //uc.Dispose();
        }

        [Test]
        public void IsServeAllPositionsWorking()
        {
            throw new System.Exception();
            //var unitsRepository = new Repository<Unit>();
            //var time = new GameTime();
            //var level = LevelDefinitionSetups.GetDefault();
            //var random = new SessionRandom();
            //var unitSettings = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            //unitSettings.Speed = 1;
            //unitSettings.SpeedUp = 1;
            //unitSettings.SpeedUpDistance = 1;
            //unitSettings.MinSpeed = 1f;
            //unitSettings.RotationSpeed = 1f;
            //IGameTime.Default = time;

            //var uc = new UnitsService(unitsRepository, unitSettings, level, random);
            //var crowd = new UnitsCrowdService(unitsRepository, uc, time, level, random);
            //var queue = new UnitsCustomerQueueService(unitsRepository, uc, crowd, time, random, new GameVector3(0, 0, 1), 1);

            //var collection = new ViewsCollection();
            //new UnitsPresenter(unitsRepository, new UnitsManagerView(collection), unitSettings);

            //queue.SetQueueSize(4);
            //queue.ServeCustomer();
            //time.MoveTime(2);

            //Assert.AreEqual(4, queue.GetUnits().Count());

            //queue.ServeAll();
            //// 1
            //Assert.AreEqual(3, queue.GetUnits().Count());
            //Assert.AreEqual(new GameVector3(1, 0, 0), queue.GetUnits().ElementAt(0).Position);
            //Assert.AreEqual(new GameVector3(2, 0, 0), queue.GetUnits().ElementAt(1).Position);
            //Assert.AreEqual(new GameVector3(3, 0, 0), queue.GetUnits().ElementAt(2).Position);
            //time.MoveTime(1f);
            //// 2
            //Assert.AreEqual(2, queue.GetUnits().Count());
            //Assert.AreEqual(new GameVector3(2, 0, 0), queue.GetUnits().ElementAt(0).Position);
            //Assert.AreEqual(new GameVector3(3, 0, 0), queue.GetUnits().ElementAt(1).Position);
            //time.MoveTime(1f);
            //// 3
            //Assert.AreEqual(1, queue.GetUnits().Count());
            //Assert.AreEqual(new GameVector3(3, 0, 0), queue.GetUnits().ElementAt(0).Position);
            //time.MoveTime(1f);
            //// 4
            //Assert.AreEqual(0, queue.GetUnits().Count());
            //collection.Dispose();
            //queue.Dispose();
            //uc.Dispose();
        }

        [Test]
        public void IsServeCoinsWorking()
        {
            throw new System.Exception();
            //var unitsRepository = new Repository<Unit>();
            //var time = new GameTime();
            //var level = LevelDefinitionSetups.GetDefault();
            //var random = new SessionRandom();
            //var unitSettings = UnitDefinitionSetup.GetDefaultUnitsDefinitions();
            //IGameTime.Default = time;

            //var uc = new UnitsService(unitsRepository, unitSettings, level, random);
            //var crowd = new UnitsCrowdService(unitsRepository, uc, time, level, random);
            //var queue = new UnitsCustomerQueueService(unitsRepository, uc, crowd, time, random, new GameVector3(0, 0, 1), 0);
            //var coins = new CoinsService();

            //var collection = new ViewsCollection();
            //new UnitsPresenter(unitsRepository, new UnitsManagerView(collection), unitSettings);

            //Assert.AreEqual(0, coins.Value);

            //queue.SetQueueSize(2);
            //queue.ServeCustomer(); // add new one
            //queue.ServeCustomer(); // serve
            //time.MoveTime(1);

            //Assert.AreEqual(1, coins.Value);

            //collection.Dispose();
            //queue.Dispose();
            //uc.Dispose();
        }

        [Test]
        public void IsFirstUnitAppearsAfterFirstBuilding()
        {
            throw new System.Exception();
            //var game = new GameConstructor()
            //    .UpdateDefinition<ConstructionsSettingsDefinition>(x => x.PieceMovingTime = 1)
            //    .UpdateDefinition<ConstructionDefinition>(x => x.Points = 3)
            //    .UpdateDefinition<LevelDefinitionMock>(x => x.CrowdUnitsAmount = 0)
            //    .Build();

            //game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            //game.Controls.Click();

            //Assert.AreEqual(1, IStageLevelService.Default.Points.GetTargetLevel());
            //Assert.AreEqual(0, IStageLevelService.Default.Points.GetCurrentLevel());
            //Assert.AreEqual(1, game.LevelCollection.FindViews<UnitView>().Count);

            //game.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
