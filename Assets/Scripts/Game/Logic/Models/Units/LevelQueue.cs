using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public class LevelQueue : Disposable
    {
        private LevelUnits _units;
        private UnitsSettingsDefinition _unitsSettings;
        private BuildingPoints _points;
        private Deck<CustomerDefinition> _pool;
        private List<Unit> _queue = new List<Unit>();

        public LevelQueue(UnitsSettingsDefinition unitsSettings, LevelUnits units, LevelDefinition levelDefinition,
            SessionRandom random, BuildingPoints points)
        {
            _units = units;
            _unitsSettings = unitsSettings ?? throw new ArgumentNullException(nameof(unitsSettings));
            _points = points ?? throw new ArgumentNullException(nameof(points));
            _points.OnPointsChanged += UpdatePoints;

            _pool = new Deck<CustomerDefinition>(random);
            foreach (var item in levelDefinition.BaseCrowdUnits)
                _pool.Add(item.Key, item.Value);

            UpdatePoints();
        }

        protected override void DisposeInner()
        {
            _points.OnPointsChanged += UpdatePoints;
            foreach (var unit in _queue)
                _units.RemoveUnit(unit);
            _queue.Clear();
        }

        private void UpdatePoints()
        {
            while (_points.CurrentLevel > _queue.Count)
            {
                var definition = _pool.Take();
                var position = new FloatPoint();
                var unit = new Unit(position, position, definition, _unitsSettings);
                _queue.Add(_units.SpawnUnit(unit));
            } 
            
            while (_points.CurrentLevel < _queue.Count)
            {
                _units.RemoveUnit(_queue.First());
            }
        }
    }
}
