using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Common;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Tests.Cases.Game.Customers
{
    public class UnitTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void IsMovementEventsWorking()
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

            units.OnModelEvent += Units_OnModelEvent;
            time.MoveTime(100);
            units.OnModelEvent -= Units_OnModelEvent;

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
    }
}
