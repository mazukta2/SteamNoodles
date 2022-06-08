using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services.Common;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Units
{
    public class UnitsService : Disposable, IService
    {
        private readonly IGameRandom _random;
        private readonly UnitsTypesService _unitsTypesService;
        private readonly float _unitSize;
        private readonly IRepository<Unit> _units;

        public UnitsService(IRepository<Unit> units, UnitsSettingsDefinition unitsSettings,
            LevelDefinition levelDefinition, IGameRandom random, UnitsTypesService unitsTypesService) 
            : this(units, random, unitsTypesService, unitsSettings.UnitSize)
        {
        }

        public UnitsService(IRepository<Unit> units, IGameRandom random, UnitsTypesService unitsTypesService, float unitSize = 1)
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
            _units.Save(unit);
        }

        public void DestroyUnit(Unit unit)
        {
            _units.Remove(unit);
        }

        public float GetUnitSize()
        {
            return _unitSize;
        }

    }
}
