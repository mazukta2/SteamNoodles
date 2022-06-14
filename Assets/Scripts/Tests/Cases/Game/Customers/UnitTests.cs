using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Events.Units;
using Game.Assets.Scripts.Game.Logic.Models.Services.Common;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Level.Units;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Customers
{
    public class UnitTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void MovementEventsWorks()
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

            var movement = new UnitsMovementsService(units, time);

            var unit = new Unit(new GameVector3(1, 0, 0), new GameVector3(0, 0, 0), type, random);
            units.Add(unit);
            var isUnitReachedPosition = false;

            units.OnEvent += Units_OnModelEvent;
            time.MoveTime(100);
            units.OnEvent -= Units_OnModelEvent;

            Assert.AreEqual(new GameVector3(0, 0, 0), units.Get(unit.Id).Position);
            Assert.IsTrue(isUnitReachedPosition);

            movement.Dispose();
            unitsService.Dispose();

            void Units_OnModelEvent(Unit arg1, IModelEvent evt)
            {
                if (evt is UnitReachedTargetPositionEvent)
                    isUnitReachedPosition = true;
            }
        }

        [Test, Order(TestCore.ModelOrder)]
        public void SpeedUpWorks()
        {
            var units = new Repository<Unit>();
            var unitTypes = new Repository<UnitType>();
            var deck = new DeckService<UnitType>();
            var type = new UnitType(new UnitSpeed(0.5f), new UnitSpeed(1), 0, 0.5f, 1, 1);
            unitTypes.Add(type);
            deck.Add(type);

            var time = new GameTime();
            var random = new SessionRandom();
            var unitTypesService = new UnitsTypesService(unitTypes, deck);
            var unitsService = new UnitsService(units, random, unitTypesService);
            var movement = new UnitsMovementsService(units, time);
            var unit = new Unit(new GameVector3(2, 0, 0), new GameVector3(0, 0, 0), type, random);
            units.Add(unit);

            var timePassed = 0.1f;
            time.MoveTime(timePassed);

            Assert.AreEqual(0.6f, units.Get(unit.Id).CurrentSpeed); // min speed(0.5) + speedup(1 per second = 0.1f) 

            // moving with current speed
            Assert.AreEqual(new GameVector3(2, 0, 0)
                .MoveTowards(new GameVector3(0, 0, 0), 0.6f * timePassed),
                units.Get(unit.Id).Position);

            time.MoveTime(0.9f);
            Assert.AreEqual(1f, units.Get(unit.Id).CurrentSpeed); // full speed

            movement.Dispose();
            unitsService.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void SpeedDownWorks()
        {
            var units = new Repository<Unit>();
            var unitTypes = new Repository<UnitType>();
            var deck = new DeckService<UnitType>();
            var type = new UnitType(new UnitSpeed(0.5f), new UnitSpeed(1), 0, 0.5f, 1, 1);
            unitTypes.Add(type);
            deck.Add(type);

            var time = new GameTime();
            var random = new SessionRandom();
            var unitTypesService = new UnitsTypesService(unitTypes, deck);
            var unitsService = new UnitsService(units, random, unitTypesService);
            var movement = new UnitsMovementsService(units, time);
            var unit = new Unit(new GameVector3(1, 0, 0), new GameVector3(0, 0, 0), type, random);
            unit.SetTargetSpeed(1);
            units.Add(unit);

            var timePassed = 0.1f;
            time.MoveTime(timePassed);

            Assert.AreEqual(0.9f, units.Get(unit.Id).CurrentSpeed); // min speed(0.5) + speedup(1 per second = 0.1f) 

            time.MoveTime(0.9f);

            Assert.AreEqual(0.5f, units.Get(unit.Id).CurrentSpeed); // full speed down

            movement.Dispose();
            unitsService.Dispose();
        }


        [Test, Order(TestCore.PresenterOrder)]
        public void RotationWorks()
        {
            var units = new Repository<Unit>();
            var unitTypes = new Repository<UnitType>();
            var deck = new DeckService<UnitType>();
            var type = new UnitType(new UnitSpeed(0.5f), new UnitSpeed(1), 0, 0.5f, 1, 1);
            unitTypes.Add(type);
            deck.Add(type);

            var time = new GameTime();
            var random = new SessionRandom();
            var unitTypesService = new UnitsTypesService(unitTypes, deck);
            var unitsService = new UnitsService(units, random, unitTypesService);
            var movement = new UnitsMovementsService(units, time);
            var unit = new Unit(new GameVector3(0, 0, 0), new GameVector3(0, 0, 0), type, random);
            var link = units.Add(unit);

            var collection = new ViewsCollection();
            var unitView = new UnitView(collection);
            new UnitPresenter(unitView, link, units, time);

            AssertHelpers.CompareVectors(new GameVector3(-1, 0, 0), unitView.Rotator.Rotation.ToVector());
            var originalRotation = unitView.Rotator.Rotation;
            var targetRotation = new GameVector3(0, 0, 1).ToQuaternion();

            unit.LookAt(new GameVector3(0, 0, 1));
            units.Save(unit);

            var timePassed = 0.1f;
            time.MoveTime(timePassed);

            var expectedRotation = GameQuaternion.RotateTowards(originalRotation, targetRotation, 0.5f * timePassed); // rotatin speed * time passed
            AssertHelpers.CompareVectors(expectedRotation.ToVector(), unitView.Rotator.Rotation.ToVector(), 0.01f);

            time.MoveTime(100);

            AssertHelpers.CompareVectors(new GameVector3(0, 0, 1), unitView.Rotator.Rotation.ToVector());

            movement.Dispose();
            unitsService.Dispose();
            collection.Dispose();
        }
    }
}
