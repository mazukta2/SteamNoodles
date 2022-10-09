using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels.Variations;
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
        private MainLevelVariation _levelDefinition;
        private readonly IUnits _unitsController;
        private IGameRandom _random;
        private IGameTime _time;

        public LevelCrowd(IUnits unitsController, IGameTime time, MainLevelVariation levelDefinition,
            IGameRandom random)
        {
            _levelDefinition = levelDefinition;
            _unitsController = unitsController ?? throw new ArgumentNullException(nameof(unitsController));
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _time = time ?? throw new ArgumentNullException(nameof(time));

            for (int i = 0; i < levelDefinition.CrowdUnitsAmount; i++)
            {
                var position = GetRandomPoint(UnitsField, _random);
                var target = GetRandomPointDirection(_random.GetRandom() ? CrowdDirection.Left : CrowdDirection.Right);
                _crowd.Add(_unitsController.SpawnUnit(position, target));
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
                if (!item.IsMoving())
                {
                    _unitsController.DestroyUnit(item);
                    _crowd.Remove(item);
                }
            }

            if (_crowd.Count < _levelDefinition.CrowdUnitsAmount)
            {
                var direction = _random.GetRandom() ? CrowdDirection.Left : CrowdDirection.Right;
                var target = GetRandomPointDirection(direction);
                var targetOposite = GetRandomPointDirection(direction == CrowdDirection.Right ? CrowdDirection.Left : CrowdDirection.Right);
                var position = new GameVector3(targetOposite.X, 0, target.Z);

                var unit = _unitsController.SpawnUnit(position, target);
                _crowd.Add(unit);
            }
        }

        private GameVector3 GetRandomPoint(FloatRect rect, IGameRandom random)
        {
            return new GameVector3(random.GetRandom(rect.xMin, rect.xMax), 0, random.GetRandom(rect.yMin, rect.yMax));
        }

        public void SendToCrowd(Unit unit, CrowdDirection direction)
        {
            unit.SetTarget(GetRandomPointDirection(direction));
            _crowd.Add(unit);
        }

        public GameVector3 GetRandomPointDirection(CrowdDirection direction)
        {
            var position = GetRandomPoint(UnitsField, _random);
            if (direction == CrowdDirection.Right)
                return new GameVector3(UnitsField.X + UnitsField.Width, 0, position.Z);
            else
                return new GameVector3(UnitsField.X, 0, position.Z);
        }

        public enum CrowdDirection
        {
            Left,
            Right
        }
    }
}
