using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
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
        private GameLevel _gameLevel;
        private LevelDefinition _levelDefinition;
        private Deck<CustomerDefinition> _pool;
        private List<Unit> _queue = new List<Unit>();
        private int _spawnedUnits;

        public LevelQueue(UnitsSettingsDefinition unitsSettings, LevelUnits units, LevelDefinition levelDefinition,
            SessionRandom random, BuildingPoints points, Levels.GameLevel gameLevel)
        {
            _units = units;
            _unitsSettings = unitsSettings ?? throw new ArgumentNullException(nameof(unitsSettings));
            _points = points ?? throw new ArgumentNullException(nameof(points));
            _gameLevel = gameLevel ?? throw new ArgumentNullException(nameof(gameLevel));
            _levelDefinition = levelDefinition ?? throw new ArgumentNullException(nameof(levelDefinition));
            _points.OnPointsChanged += HandlePointsChanged;
            _gameLevel.OnTurn += HandleTurn;

            _pool = new Deck<CustomerDefinition>(random);
            foreach (var item in levelDefinition.BaseCrowdUnits)
                _pool.Add(item.Key, item.Value);

            HandlePointsChanged();
        }

        protected override void DisposeInner()
        {
            _points.OnPointsChanged += HandlePointsChanged;
            _gameLevel.OnTurn -= HandleTurn;
            foreach (var unit in _queue)
                _units.RemoveUnit(unit);
            _queue.Clear();
        }

        private void HandlePointsChanged()
        {
            while (_points.CurrentLevel > _spawnedUnits)
            {
                _spawnedUnits++;

                var definition = _pool.Take();
                var position = _levelDefinition.QueuePosition + new FloatPoint(0.5f, 0) * (_queue.Count - 1);
                var unit = new Unit(position, position, definition, _unitsSettings);
                _queue.Add(_units.SpawnUnit(unit));
            }
        }

        private void HandleTurn()
        {
            if (_queue.Count == 0)
                return;

            _units.RemoveUnit(_queue.First());
        }

    }
}
