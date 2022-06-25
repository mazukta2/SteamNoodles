using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Events.Units;
using Game.Assets.Scripts.Game.Logic.Repositories;
using static Game.Assets.Scripts.Game.Logic.Models.Entities.Units.Unit;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Units.QueueAnimations
{
    public class MoveUnitsToPositionsInQueue : BaseSequenceStep
    {
        private readonly IRepository<Unit> _units;
        private readonly UnitsService _unitsService;
        private readonly UnitsCustomerQueueService _queue;

        public MoveUnitsToPositionsInQueue(IRepository<Unit> units, UnitsService unitsService,
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

        private void HandleEvent(Unit eventUnit, IModelEvent modelEvent)
        {
            if (modelEvent is not UnitReachedTargetPositionEvent)
                return;

            if (eventUnit.State != BehaviourState.InQueue)
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
