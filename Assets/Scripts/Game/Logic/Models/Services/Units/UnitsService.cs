using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Units
{
    public class UnitsService : Disposable
    {
        private readonly UnitsSettingsDefinition _unitsSettings;
        private readonly IGameRandom _random;
        private Deck<CustomerDefinition> _pool; // TODO: to repository
        private readonly IRepository<Unit> _units;

        public UnitsService(IRepository<Unit> units, UnitsSettingsDefinition unitsSettings, LevelDefinition levelDefinition, IGameRandom random)
        {
            _units = units ?? throw new ArgumentNullException(nameof(units));
            _unitsSettings = unitsSettings ?? throw new ArgumentNullException(nameof(unitsSettings));
            _random = random ?? throw new ArgumentNullException(nameof(random));

            _pool = new Deck<CustomerDefinition>(random);
            foreach (var item in levelDefinition.BaseCrowdUnits)
                _pool.Add(item.Key, item.Value);
        }

        protected override void DisposeInner()
        {
        }

        public Unit SpawnUnit(GameVector3 pos)
        {
            return SpawnUnit(pos, pos, _pool.Take());
        }

        public Unit SpawnUnit(GameVector3 pos, GameVector3 target)
        {
            return SpawnUnit(pos, target, _pool.Take());
        }

        public Unit SpawnUnit(GameVector3 position, GameVector3 target, CustomerDefinition definition)
        {
            var unit = new Unit(position, target, definition, _unitsSettings, _random);
            _units.Add(unit);
            return unit;
        }

        public void Smoke(Unit unit)
        {
            _units.FireEvent(unit, new UnitSmokeEvent());
        }

        public void LookAt(Unit unit, GameVector3 target, bool skip = false)
        {
            if (unit.IsMoving())
                throw new Exception("Can't look while moving");

            if ((target - unit.Position).IsZero())
                throw new Exception("Wrong target");

            _units.FireEvent(unit, new UnitLookAtEvent(target, skip));
        }

        public void DestroyUnit(Unit unit)
        {
            _units.Remove(unit);
        }

        public float GetUnitSize()
        {
            return _unitsSettings.UnitSize;
        }

    }
}
