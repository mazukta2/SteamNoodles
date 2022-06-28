using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Repositories;
using NUnit.Framework;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Services.Common;
using Game.Assets.Scripts.Game.Logic.Services.Session;
using Game.Assets.Scripts.Game.Logic.Services.Units;

namespace Game.Assets.Scripts.Tests.Cases.Game.Customers
{
    public class CrowdTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void IsCrowdUnitsSpawned()
        {
            var units = new Database<Unit>();
            var unitTypes = new Database<UnitType>();
            var deck = new DeckService<UnitType>();
            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var time = new GameTime();
            var random = new SessionRandom();

            var unitTypesService = new UnitsTypesService(unitTypes, deck);

            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, GetRect(), 15);

            Assert.AreEqual(15, units.Count);

            unitsService.Dispose();
            crowd.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsCrowdDestroying()
        {
            var units = new Database<Unit>();
            var unitTypes = new Database<UnitType>();
            var deck = new DeckService<UnitType>();
            var type = new UnitType();
            unitTypes.Add(type);
            deck.Add(type);

            var time = new GameTime();
            var random = new SessionRandom();

            var unitTypesService = new UnitsTypesService(unitTypes, deck);

            var movement = new UnitsMovementsService(units, time);
            var unitsService = new UnitsService(units, random, unitTypesService);
            var crowd = new UnitsCrowdService(units, unitsService, time, random, new FloatRect(-10, -10, 10, 10), 1);

            Assert.AreEqual(1, units.Count);
            var id = units.Get().First().Id;
            time.MoveTime(100);
            Assert.AreEqual(1, units.Count);
            var id2 = units.Get().First().Id;
            Assert.AreNotEqual(id, id2);

            unitsService.Dispose();
            crowd.Dispose();
            movement.Dispose();
        }

        private FloatRect GetRect() => new FloatRect(0, 0, 10, 10);

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
