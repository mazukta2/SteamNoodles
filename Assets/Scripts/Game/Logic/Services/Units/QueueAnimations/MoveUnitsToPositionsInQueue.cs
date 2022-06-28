using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Events.Units;
using static Game.Assets.Scripts.Game.Logic.Entities.Units.UnitEntity;

namespace Game.Assets.Scripts.Game.Logic.Services.Units.QueueAnimations
{
    public class MoveUnitsToPositionsInQueue : BaseSequenceStep
    {
        private readonly IDatabase<UnitEntity> _units;
        private readonly UnitsService _unitsService;
        private readonly UnitsCustomerQueueService _queue;

        public MoveUnitsToPositionsInQueue(IDatabase<UnitEntity> units, UnitsService unitsService,
            UnitsCustomerQueueService queue)
        {
            _units = units;
            _unitsService = unitsService;
            _queue = queue;
        }

        protected override void DisposeInner()
        {
            _units.OnEvent -= HandleEvent;
        }

        public override void Play()
        {
            _units.OnEvent += HandleEvent;

            var list = _queue.GetUnits().ToArray();
            for (int i = 0; i < list.Count(); i++)
            {
                list[i].SetTarget(_queue.GetPositionFor(i));
            }
            if (list.Length == 0)
                FireOnFinished();
        }

        private void HandleEvent(UnitEntity eventUnitEntity, IModelEvent modelEvent)
        {
            if (modelEvent is not UnitReachedTargetPositionEvent)
                return;

            if (eventUnitEntity.State != BehaviourState.InQueue)
                return;

            var list = _queue.GetUnits().ToArray();
            var first = list[0];
            if (!first.IsMoving())
            {
                _unitsService.LookAt(first, _queue.GetQueuePosition() + _queue.GetQueueFirstPositionOffset() + new GameVector3(0, 0, 1));
            }

            if (list.All(x => !x.IsMoving()))
                FireOnFinished();
        }


    }
}
