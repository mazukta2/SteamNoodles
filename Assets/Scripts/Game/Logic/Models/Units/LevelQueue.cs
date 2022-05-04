using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Building;
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
        private FlowManager _turnManager;
        private LevelDefinition _levelDefinition;
        private PlacementField _constructionsManager;
        private SessionRandom _random;
        private Deck<CustomerDefinition> _pool;
        private List<Unit> _queue = new List<Unit>();
        private float _queueStartingPosition;

        public LevelQueue(UnitsSettingsDefinition unitsSettings, 
            LevelUnits units, LevelDefinition levelDefinition,
            SessionRandom random, BuildingPoints points,
            PlacementField constructionsManager,
            FlowManager turnManager)
        {
            _units = units;
            _unitsSettings = unitsSettings ?? throw new ArgumentNullException(nameof(unitsSettings));
            _points = points ?? throw new ArgumentNullException(nameof(points));
            _turnManager = turnManager ?? throw new ArgumentNullException(nameof(turnManager));
            _levelDefinition = levelDefinition ?? throw new ArgumentNullException(nameof(levelDefinition));
            _constructionsManager = constructionsManager ?? throw new ArgumentNullException(nameof(constructionsManager));
            _random = random;
            
            _points.OnLevelUp += _points_OnLevelUp;
            _points.OnLevelDown += _points_OnLevelDown;
            _turnManager.OnTurn += HandleTurn;
            _constructionsManager.OnConstructionAdded += Placement_OnConstructionAdded;

            _pool = new Deck<CustomerDefinition>(random);
            foreach (var item in levelDefinition.BaseCrowdUnits)
                _pool.Add(item.Key, item.Value);
        }

        protected override void DisposeInner()
        {
            _constructionsManager.OnConstructionAdded -= Placement_OnConstructionAdded;
            _points.OnLevelUp -= _points_OnLevelUp;
            _points.OnLevelDown -= _points_OnLevelDown;
            _turnManager.OnTurn -= HandleTurn;
            foreach (var unit in _queue)
                _units.RemoveUnit(unit);
            _queue.Clear();
        }

        private void Placement_OnConstructionAdded(Construction construction)
        {
            _queueStartingPosition = construction.GetWorldPosition().X;
        }

        private void HandleTurn()
        {
        }

        private void _points_OnLevelUp()
        {
            var definition = _pool.Take();
            var position = _levelDefinition.QueuePosition + new FloatPoint(_unitsSettings.UnitSize, 0) * (_queue.Count - 1);
            if (_queue.Count == 0)
                position.X = _queueStartingPosition;
            var unit = new Unit(position, position, definition, _unitsSettings, _random);
            _queue.Add(_units.SpawnUnit(unit));
        }

        private void _points_OnLevelDown()
        {
            var unit = _queue.Last();
            _queue.Remove(unit);
            _units.RemoveUnit(unit);
        }

    }
}
