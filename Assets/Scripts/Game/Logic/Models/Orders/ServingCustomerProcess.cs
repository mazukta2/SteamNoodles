using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Effects.Systems;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features;
using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using static Game.Assets.Scripts.Game.Logic.Models.Units.Unit;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class ServingCustomerProcess : Disposable
    {
        public event Action<ServingCustomerProcess> OnJoinQueue = delegate { };
        public event Action<ServingCustomerProcess> OnWaitCooking = delegate { };
        public event Action<ServingCustomerProcess> OnStartEating = delegate { };
        public event Action<ServingCustomerProcess> OnFinished = delegate { };
        public event Action<ServingCustomerProcess> OnCanceled = delegate { };
        public Unit Unit { get; internal set; }
        public Phase CurrentPhase => _stateMachine.State;

        private UnitPlacement _unitPlacement;
        private GameTime _time;
        private GameLevel _level;
        private LevelUnits _units;
        private GameTimer _timer;
        private StateMachine<Phase, Triggers> _stateMachine = new StateMachine<Phase, Triggers>(Phase.Idle);

        public ServingCustomerProcess(UnitPlacement unitPlacement, GameTime time, GameLevel level, LevelUnits units, Unit unit)
        {
            _time = time;
            _level = level;
            _units = units;
            Unit = unit;
            _unitPlacement = unitPlacement;

            _stateMachine.Configure(Phase.Idle)
                .Permit(Triggers.Start, Phase.MovingTo);

            _stateMachine.Configure(Phase.MovingTo)
                .OnEntry(MovingToStarted)
                .Permit(Triggers.WalkingFinished, Phase.InQueue);

            _stateMachine.Configure(Phase.InQueue)
                .OnEntry(QueueStarted)
                .Permit(Triggers.CanOrder, Phase.Ordering);

            _stateMachine.Configure(Phase.Ordering)
                .OnEntry(OrderingStarted)
                .Permit(Triggers.TimerIsEnded, Phase.WaitCooking);

            _stateMachine.Configure(Phase.WaitCooking)
                .OnEntry(WaitCookingStarted)
                .Permit(Triggers.TimerIsEnded, Phase.Eating);

            _stateMachine.Configure(Phase.Eating)
                .OnEntry(EatingStart)
                .Permit(Triggers.TimerIsEnded, Phase.MovingAway);

            _stateMachine.Configure(Phase.MovingAway)
                .OnEntry(MovingAwayStart)
                .Permit(Triggers.WalkingFinished, Phase.Exiting);

            _stateMachine.Configure(Phase.Exiting)
                .OnEntry(Exiting)
                .OnEntry(Finish);


            _stateMachine.Fire(Triggers.Start);
        }

        protected override void DisposeInner()
        {
            _units.ReturnToCrowd(Unit);
            _stateMachine.Deactivate();

            if (_timer != null)
                _timer.OnFinished -= _timer_OnFinished;

            Unit.OnReachedPosition -= Unit_OnPositionReached;
        }

        //
        private void MovingToStarted()
        {
            _units.TakeFromCrowd(Unit);
            Unit.OnReachedPosition += Unit_OnPositionReached;
            Unit.SetTarget(_unitPlacement.GetServingPoint());
        }

        private void QueueStarted()
        {
            Unit.OnReachedPosition -= Unit_OnPositionReached;
            OnJoinQueue(this);
        }

        public void MoveToOdering()
        {
            _stateMachine.Fire(Triggers.CanOrder);
        }

        private void OrderingStarted()
        {
            _unitPlacement.PlaceToOrderingTable(this);
            _timer = new GameTimer(_time, Unit.GetOrderingTime());
            _timer.OnFinished += _timer_OnFinished;
        }

        private void WaitCookingStarted()
        {
            _unitPlacement.ClearPlacing(this);
            var places = _unitPlacement.GetFreePlacesToEat();
            if (places.Count == 0)
            {
                _unitPlacement.PlaceToTable(this, _unitPlacement.GetOrderingPlace());
            }
            else
            {
                _unitPlacement.PlaceToTable(this, places.First());
            }

            _timer.OnFinished -= _timer_OnFinished;
            _timer = new GameTimer(_time, Unit.GetCookingTime());
            _timer.OnFinished += _timer_OnFinished;
            OnWaitCooking(this);
        }

        private void EatingStart()
        {
            _timer.OnFinished -= _timer_OnFinished;
            _timer = new GameTimer(_time, Unit.GetEatingTime());
            _timer.OnFinished += _timer_OnFinished;
            OnStartEating(this);
        }

        private void MovingAwayStart()
        {
            _timer.OnFinished -= _timer_OnFinished;
            _timer = null;
            _unitPlacement.ClearPlacing(this);
            Unit.OnReachedPosition += Unit_OnPositionReached;
            Unit.SetTarget(_unitPlacement.GetAwayPoint());
        }

        private void Exiting()
        {
            Unit.OnReachedPosition -= Unit_OnPositionReached;
        }

        private void Unit_OnPositionReached()
        {
            _stateMachine.Fire(Triggers.WalkingFinished);
        }

        private void _timer_OnFinished()
        {
            _stateMachine.Fire(Triggers.TimerIsEnded);
        }

        private void Finish()
        {
            _level.ChangeMoney(Unit.GetServingMoney());
            _level.ChangeMoney(Unit.GetTips());
            Unit.SetServed();
            OnFinished(this);
        }

        public void CancelWithReturns()
        {
            _level.ChangeMoney(Unit.GetServingMoney());
            _level.ChangeMoney(Unit.GetTips());
            Unit.SetServed();
            OnCanceled(this);
        }

        private enum Triggers
        {
            WalkingFinished,
            TimerIsEnded,
            CanOrder,
            Start
        }

        public enum Phase
        {
            Idle,
            MovingTo,
            InQueue,
            Ordering,
            WaitCooking,
            Eating,
            MovingAway,
            Exiting,
        }
    }
}
