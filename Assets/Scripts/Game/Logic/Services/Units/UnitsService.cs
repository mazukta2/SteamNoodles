using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Events.Units;
using Game.Assets.Scripts.Game.Logic.Services.Session;

namespace Game.Assets.Scripts.Game.Logic.Services.Units
{
    public class UnitsService : Disposable, IService
    {
        private readonly IGameRandom _random;
        private readonly UnitsTypesService _unitsTypesService;
        private readonly float _unitSize;
        private readonly IDatabase<Unit> _units;

        public UnitsService(IDatabase<Unit> units, UnitsSettingsDefinition unitsSettings,
            LevelDefinition levelDefinition, IGameRandom random, UnitsTypesService unitsTypesService) 
            : this(units, random, unitsTypesService, unitsSettings.UnitSize)
        {
        }

        public UnitsService(IDatabase<Unit> units, IGameRandom random, UnitsTypesService unitsTypesService, float unitSize = 1)
        {
            _units = units ?? throw new ArgumentNullException(nameof(units));
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _unitsTypesService = unitsTypesService ?? throw new ArgumentNullException(nameof(unitsTypesService));
            _unitSize = unitSize;
        }

        protected override void DisposeInner()
        {
        }

        public Unit SpawnUnit(GameVector3 pos)
        {
            return SpawnUnit(pos, pos, _unitsTypesService.TakeRandom());
        }

        public Unit SpawnUnit(GameVector3 pos, GameVector3 target)
        {
            return SpawnUnit(pos, target, _unitsTypesService.TakeRandom());
        }

        public Unit SpawnUnit(GameVector3 position, GameVector3 target, UnitType type)
        {
            var unit = new Unit(position, target, type, _random);
            _units.Add(unit);
            return unit;
        }

        public void Smoke(Unit unit)
        {
            _units.FireEvent(unit, new UnitSmokeEvent());
        }

        public void LookAt(Unit unit, GameVector3 target, bool skip = false)
        {
            unit.LookAt(target, skip);
        }

        public void DestroyUnit(Unit unit)
        {
            _units.Remove(unit.Id);
        }

        public float GetUnitSize()
        {
            return _unitSize;
        }

    }
}
