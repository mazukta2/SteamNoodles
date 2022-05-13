using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
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
    public class LevelCrowd : Disposable, ICrowd
    {
        private FloatRect UnitsField => _levelDefinition.UnitsRect;
        private List<Unit> _crowd = new List<Unit>();
        private LevelDefinition _levelDefinition;
        private readonly IUnits _unitsController;
        private SessionRandom _random;
        private IGameTime _time;

        public LevelCrowd(IUnits unitsController, IGameTime time, LevelDefinition levelDefinition,
            SessionRandom random)
        {
            _levelDefinition = levelDefinition;
            _unitsController = unitsController ?? throw new ArgumentNullException(nameof(unitsController));
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _time = time ?? throw new ArgumentNullException(nameof(time));

            for (int i = 0; i < levelDefinition.CrowdUnitsAmount; i++)
            {
                var position = GetRandomPoint(UnitsField, _random);
                _crowd.Add(_unitsController.SpawnUnit(position,
                    new FloatPoint(_random.GetRandom() ? UnitsField.X - 1 : UnitsField.X + UnitsField.Width + 1, position.Y)));
            }
            _time.OnTimeChanged += Time_OnTimeChanged;
        }

        protected override void DisposeInner()
        {
            _time.OnTimeChanged -= Time_OnTimeChanged;
        }

        private void Time_OnTimeChanged(float oldTime, float newTime)
        {
            foreach (var item in _crowd.ToArray())
            {
                if (!IsHorisontalyInside(UnitsField, item.Position))
                {
                    _unitsController.DestroyUnit(item);
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

                var unit = _unitsController.SpawnUnit(position, target);
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

        public void SendToCrowd(Unit unit, CrowdDirection direction)
        {
            var position = GetRandomPoint(UnitsField, _random);
            FloatPoint target;
            if (direction == CrowdDirection.Right)
                target = new FloatPoint(UnitsField.X + UnitsField.Width + 1, position.Y);
            else
                target = new FloatPoint(UnitsField.X - 1, position.Y);
            unit.SetTarget(target);
            _crowd.Add(unit);
        }

        public enum CrowdDirection
        {
            Left,
            Right
        }
    }
}
