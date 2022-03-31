using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public class LevelCrowd : Disposable
    {
        private FloatRect UnitsField => _levelDefinition.UnitsRect;
        private List<Unit> _crowd = new List<Unit>();
        private LevelDefinition _levelDefinition;
        private LevelUnits _units;
        private UnitsSettingsDefinition _unitsSettings;
        private SessionRandom _random;
        private GameTime _time;
        private Deck<CustomerDefinition> _pool;

        public LevelCrowd(UnitsSettingsDefinition unitsSettings, LevelUnits units, GameTime time, LevelDefinition levelDefinition,
            SessionRandom random)
        {
            _levelDefinition = levelDefinition;
            _units = units;
            _unitsSettings = unitsSettings ?? throw new ArgumentNullException(nameof(unitsSettings));
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _time = time ?? throw new ArgumentNullException(nameof(time));

            _pool = new Deck<CustomerDefinition>(random);
            foreach (var item in levelDefinition.BaseCrowdUnits)
                _pool.Add(item.Key, item.Value);

            for (int i = 0; i < levelDefinition.CrowdUnitsAmount; i++)
            {
                var definition = _pool.Take();
                var position = GetRandomPoint(UnitsField, _random);
                var unit = new Unit(position, new FloatPoint(_random.GetRandom() ? UnitsField.X - 1 : UnitsField.X + UnitsField.Width + 1, position.Y), 
                    definition, _unitsSettings);
                _crowd.Add(units.SpawnUnit(unit));
            }
            _time.OnTimeChanged += Time_OnTimeChanged;
        }

        protected override void DisposeInner()
        {
            _time.OnTimeChanged -= Time_OnTimeChanged;
            foreach (var unit in _crowd)
                _units.RemoveUnit(unit);
            _crowd.Clear();
        }

        private void Time_OnTimeChanged(float delta)
        {
            foreach (var item in _crowd.ToArray())
            {
                if (!IsHorisontalyInside(UnitsField, item.Position))
                {
                    _units.RemoveUnit(item);
                    _crowd.Remove(item);
                }
            }

            if (_crowd.Count < _levelDefinition.CrowdUnitsAmount)
            {
                var position = GetRandomPoint(UnitsField, _random);
                FloatPoint target;
                if (_random.GetRandom())
                {
                    position = new FloatPoint(UnitsField.X + 1, position.Y);
                    target = new FloatPoint(UnitsField.X + UnitsField.Width + 1, position.Y);
                }
                else
                {
                    position = new FloatPoint(UnitsField.X + UnitsField.Width - 1, position.Y);
                    target = new FloatPoint(UnitsField.X - 1, position.Y);
                }

                var unit = _units.SpawnUnit(new Unit(position, target, _pool.Take(), _unitsSettings));
                _crowd.Add(unit);
            }
        }
        public bool IsHorisontalyInside(FloatRect rect, FloatPoint point)
        {
            return rect.xMin <= point.X && point.X <= rect.xMax;
        }

        private FloatPoint GetRandomPoint(FloatRect rect, SessionRandom random)
        {
            return new FloatPoint(random.GetRandom(rect.xMin, rect.xMax), random.GetRandom(rect.yMin, rect.yMax));
        }


    }
}
