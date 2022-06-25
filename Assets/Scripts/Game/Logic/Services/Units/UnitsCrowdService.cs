using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Services.Session;

namespace Game.Assets.Scripts.Game.Logic.Services.Units
{
    public class UnitsCrowdService : Disposable, IService
    {
        private readonly IRepository<Unit> _units;
        private readonly UnitsService _unitsService;
        private readonly IGameRandom _random;
        private readonly IGameTime _time;
        private readonly int _crowdAmount;
        private readonly FloatRect _unitRect;

        public UnitsCrowdService(IRepository<Unit> units, UnitsService unitsService, IGameTime time, StageLevel stageLevel,
            IGameRandom random) : this(units, unitsService, time, random,
                stageLevel.UnitsRect,
                stageLevel.CrowdUnitsAmount)
        {
        }

        public UnitsCrowdService(IRepository<Unit> units, UnitsService unitsService, IGameTime time,
            IGameRandom random, FloatRect rect, int crowdAmount = 0)
        {
            _units = units;
            _unitsService = unitsService ?? throw new ArgumentNullException(nameof(unitsService));
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _time = time ?? throw new ArgumentNullException(nameof(time));
            _crowdAmount = crowdAmount;
            _unitRect = rect;

            for (int i = 0; i < crowdAmount; i++)
            {
                var position = GetRandomPoint(GetUnitsField(), _random);
                var target = GetRandomPointDirection(_random.GetRandom() ? CrowdDirection.Left : CrowdDirection.Right);
                var unit = _unitsService.SpawnUnit(position, target);
                unit.SetBehaviourState(Unit.BehaviourState.InCrowd);
            }
            _time.OnTimeChanged += Time_OnTimeChanged;
        }

        protected override void DisposeInner()
        {
            _time.OnTimeChanged -= Time_OnTimeChanged;
        }

        private FloatRect GetUnitsField()
        {
            return _unitRect;
        }

        private void Time_OnTimeChanged(float oldTime, float newTime)
        {
            foreach (var unit in GetUnits())
            {
                if (!unit.IsMoving())
                {
                    _unitsService.DestroyUnit(unit);
                }
            }

            if (GetUnits().Count < _crowdAmount)
            {
                var direction = _random.GetRandom() ? CrowdDirection.Left : CrowdDirection.Right;
                var target = GetRandomPointDirection(direction);
                var targetOposite = GetRandomPointDirection(direction == CrowdDirection.Right ? CrowdDirection.Left : CrowdDirection.Right);
                var position = new GameVector3(targetOposite.X, 0, target.Z);

                var unit = _unitsService.SpawnUnit(position, target);
                unit.SetBehaviourState(Unit.BehaviourState.InCrowd);
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
        }

        public GameVector3 GetRandomPointDirection(CrowdDirection direction)
        {
            var position = GetRandomPoint(GetUnitsField(), _random);
            if (direction == CrowdDirection.Right)
                return new GameVector3(GetUnitsField().X + GetUnitsField().Width, 0, position.Z);
            else
                return new GameVector3(GetUnitsField().X, 0, position.Z);
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
