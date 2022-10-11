﻿using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public class LevelUnits : Disposable, IUnits
    {
        public event Action<Unit> OnUnitSpawn = delegate { };
        public event Action<Unit> OnUnitDestroy = delegate { };
        public IReadOnlyCollection<Unit> Units => _spawnedUnits;

        private List<Unit> _spawnedUnits = new List<Unit>();
        private IGameTime _time;
        private Deck<CustomerDefinition> _pool;
        private readonly UnitsSettingsDefinition _unitsSettings;
        private readonly IGameRandom _random;

        public LevelUnits(IGameTime time, UnitsSettingsDefinition unitsSettings, MainLevelVariation levelDefinition, IGameRandom random)
        {
            _time = time ?? throw new ArgumentNullException(nameof(time));
            _unitsSettings = unitsSettings ?? throw new ArgumentNullException(nameof(unitsSettings));
            _random = random ?? throw new ArgumentNullException(nameof(random));

            _pool = new Deck<CustomerDefinition>(random);
            foreach (var item in levelDefinition.BaseCrowdUnits)
                _pool.Add(item.Key, item.Value);
        }

        protected override void DisposeInner()
        {
            foreach (var unit in _spawnedUnits)
                unit.Dispose();
        }

        public Unit SpawnUnit(GameVector3 pos)
        {
            return SpawnUnit(pos, _pool.Take());
        }

        public Unit SpawnUnit(GameVector3 position, CustomerDefinition definition)
        {
            return SpawnUnit(new Unit(position, position, definition, _unitsSettings, _random, _time));
        }

        public Unit SpawnUnit(Unit unit)
        {
            _spawnedUnits.Add(unit);
            OnUnitSpawn(unit);
            return unit;
        }

        public void DestroyUnit(Unit unit)
        {
            _spawnedUnits.Remove(unit);
            unit.Dispose();
            OnUnitDestroy(unit);
        }

        public float GetUnitSize()
        {
            return _unitsSettings.UnitSize;
        }

    }
}