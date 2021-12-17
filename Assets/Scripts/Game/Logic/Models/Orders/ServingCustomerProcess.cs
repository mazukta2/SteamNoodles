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

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class ServingCustomerProcess : Disposable
    {
        public event Action OnFinished = delegate { };
        public event Action OnCanceled = delegate { };
        public Unit Unit { get; internal set; }

        private CustomerManager _customerManager;

        public Phase CurrentPhase => _stateMachine.State;

        private Placement _placement;
        private GameTime _time;
        private GameLevel _level;
        private GameTimer _timer;
        StateMachine<Phase, Triggers> _stateMachine = new StateMachine<Phase, Triggers>(Phase.Idle);

        public ServingCustomerProcess(CustomerManager manager, GameTime time, Placement placement, GameLevel level, Unit unit)
        {
            _placement = placement;
            _time = time;
            _level = level;
            Unit = unit;
            _customerManager = manager;

            _stateMachine.Configure(Phase.Idle)
                .Permit(Triggers.Start, Phase.MovingTo);

            _stateMachine.Configure(Phase.MovingTo)
                .OnEntry(MovingToStarted)
                .Permit(Triggers.WalkingFinished, Phase.Ordering);

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
                .OnEntry(Finish);

            Unit.OnReachedPosition += Unit_OnPositionReached;

            _stateMachine.Fire(Triggers.Start);
        }

        protected override void DisposeInner()
        {
            _stateMachine.Deactivate();
            Unit.OnReachedPosition -= Unit_OnPositionReached;

            if (_timer != null)
                _timer.OnFinished -= _timer_OnFinished;
        }

        //
        private void MovingToStarted()
        {
            Unit.SetTarget(GetServingPoint());
        }

        private void QueueStarted()
        {
            _stateMachine.Fire(Triggers.CanOrder);
        }

        private void OrderingStarted()
        {
            _timer = new GameTimer(_time, Unit.GetOrderingTime());
            _timer.OnFinished += _timer_OnFinished;
        }

        private void WaitCookingStarted()
        {
            var places = _customerManager.GetFreePlacesToEat();
            if (places.Count == 0)
            {
                _customerManager.Occupy(this, _customerManager.GetOrderingPlace());
            }
            else
            {
                _customerManager.Occupy(this, places.First());
            }

            _timer.OnFinished -= _timer_OnFinished;
            _timer = new GameTimer(_time, Unit.GetCookingTime());
            _timer.OnFinished += _timer_OnFinished;
        }

        private void EatingStart()
        {
            _timer.OnFinished -= _timer_OnFinished;
            _timer = new GameTimer(_time, Unit.GetEatingTime());
            _timer.OnFinished += _timer_OnFinished;
        }

        private void MovingAwayStart()
        {
            _customerManager.ClearPlacing(this);
            Unit.SetTarget(new FloatPoint(0, _placement.RealRect.yMin - _placement.CellSize * 4f));
        }

        private void Unit_OnPositionReached()
        {
            _stateMachine.Fire(Triggers.WalkingFinished);
        }

        private FloatPoint GetServingPoint()
        {
            return new FloatPoint(0, _placement.RealRect.yMin - _placement.CellSize * 1.5f);
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
            OnFinished();
        }

        public void Cancel()
        {
            _level.ChangeMoney(Unit.GetServingMoney());
            _level.ChangeMoney(Unit.GetTips());
            Unit.SetServed();
            OnCanceled();
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
