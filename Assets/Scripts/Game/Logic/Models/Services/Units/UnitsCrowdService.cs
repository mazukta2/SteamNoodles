using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Units
{
    public class UnitsCrowdService : Disposable
    {
        private readonly LevelDefinition _levelDefinition;
        private readonly IRepository<Unit> _units;
        private readonly UnitsService _unitsService;
        private readonly IGameRandom _random;
        private readonly IGameTime _time;

        public UnitsCrowdService(IRepository<Unit> units, UnitsService unitsService, IGameTime time, LevelDefinition levelDefinition,
            IGameRandom random)
        {
            _levelDefinition = levelDefinition;
            _units = units;
            _unitsService = unitsService ?? throw new ArgumentNullException(nameof(unitsService));
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _time = time ?? throw new ArgumentNullException(nameof(time));

            for (int i = 0; i < levelDefinition.CrowdUnitsAmount; i++)
            {
                var position = GetRandomPoint(UnitsField, _random);
                var target = GetRandomPointDirection(_random.GetRandom() ? CrowdDirection.Left : CrowdDirection.Right);
                var unit = _unitsService.SpawnUnit(position, target);
                unit.SetBehaviourState(Unit.BehaviourState.InCrowd);
                _units.Save(unit);
            }
            _time.OnTimeChanged += Time_OnTimeChanged;
        }

        protected override void DisposeInner()
        {
            _time.OnTimeChanged -= Time_OnTimeChanged;
        }

        private FloatRect UnitsField => _levelDefinition.UnitsRect;

        private void Time_OnTimeChanged(float oldTime, float newTime)
        {
            foreach (var unit in GetUnits())
            {
                if (!unit.IsMoving())
                {
                    _unitsService.DestroyUnit(unit);
                }
            }

            if (GetUnits().Count < _levelDefinition.CrowdUnitsAmount)
            {
                var direction = _random.GetRandom() ? CrowdDirection.Left : CrowdDirection.Right;
                var target = GetRandomPointDirection(direction);
                var targetOposite = GetRandomPointDirection(direction == CrowdDirection.Right ? CrowdDirection.Left : CrowdDirection.Right);
                var position = new GameVector3(targetOposite.X, 0, target.Z);

                var unit = _unitsService.SpawnUnit(position, target);
                unit.SetBehaviourState(Unit.BehaviourState.InCrowd);
                _units.Save(unit);
            }
        }

        private GameVector3 GetRandomPoint(FloatRect rect, IGameRandom random)
        {
            return new GameVector3(random.GetRandom(rect.xMin, rect.xMax), 0, random.GetRandom(rect.yMin, rect.yMax));
        }

        public void SendToCrowd(Unit unit, CrowdDirection direction)
        {
            unit.SetBehaviourState(Unit.BehaviourState.InCrowd);
            unit.SetTarget(GetRandomPointDirection(direction));
            _units.Save(unit);
            _units.FireEvent(unit, new UnitTargetChangedEvent());
        }

        public GameVector3 GetRandomPointDirection(CrowdDirection direction)
        {
            var position = GetRandomPoint(UnitsField, _random);
            if (direction == CrowdDirection.Right)
                return new GameVector3(UnitsField.X + UnitsField.Width, 0, position.Z);
            else
                return new GameVector3(UnitsField.X, 0, position.Z);
        }

        public IReadOnlyCollection<Unit> GetUnits()
        {
            return _units.Get().Where(x => x.State == Unit.BehaviourState.InCrowd).AsReadOnly();
        }

        public enum CrowdDirection
        {
            Left,
            Right
        }
    }
}
