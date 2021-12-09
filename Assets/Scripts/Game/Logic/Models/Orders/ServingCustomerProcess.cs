﻿using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Effects.Systems;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Stateless;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class ServingCustomerProcess : Disposable
    {
        public event Action OnFinished = delegate { };
        public event Action OnCanceled = delegate { };
        public Unit Unit { get; internal set; }

        private Placement _placement;
        private GameTime _time;
        private GameLevel _level;
        private UnitServingMoneyCalculator _moneyEffectCalculator;
        private GameTimer _timer;
        StateMachine<State, Triggers> _stateMachine = new StateMachine<State, Triggers>(State.Idle);

        public ServingCustomerProcess(GameTime time, UnitServingMoneyCalculator _moneyCalculator, Placement placement, GameLevel level, Unit unit)
        {
            _placement = placement;
            _time = time;
            _level = level;
            _moneyEffectCalculator = _moneyCalculator;
            Unit = unit;

            _stateMachine.Configure(State.Idle)
                .Permit(Triggers.Start, State.WalkingTo);
            _stateMachine.Configure(State.WalkingTo)
                .Permit(Triggers.WalkingFinished, State.Staying)
                .OnEntry(Step_1_MoveToServingPoint);
            _stateMachine.Configure(State.Staying)
                .Permit(Triggers.TimerIsEnded, State.WaklkingAway)
                .OnEntry(Step_2_WaitForServing)
                .OnExit(Step_2_EndOfServing);
            _stateMachine.Configure(State.WaklkingAway)
                .Permit(Triggers.WalkingFinished, State.Exiting)
                .OnEntry(Step_3_MoveAway);
            _stateMachine.Configure(State.Exiting)
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
        private void Step_1_MoveToServingPoint()
        {
            Unit.SetTarget(GetServingPoint());
        }
        
        private void Step_2_WaitForServing()
        {
            _timer = new GameTimer(_time, 3f);
            _timer.OnFinished += _timer_OnFinished;
        }

        private void Step_2_EndOfServing()
        {
            _timer = null;
        }

        private void Step_3_MoveAway()
        {
            Unit.SetTarget(new FloatPoint(0, _placement.RealRect.yMin - _placement.CellSize * 4f));
        }
        //

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
            _level.ChangeMoney(_moneyEffectCalculator.GetServingMoney(Unit));
            Unit.SetServed();
            OnFinished();
        }

        public void Cancel()
        {
            _level.ChangeMoney(_moneyEffectCalculator.GetServingMoney(Unit));
            Unit.SetServed();
            OnCanceled();
        }

        private enum State
        {
            Idle,
            WalkingTo,
            Staying,
            WaklkingAway,
            Exiting,
        }

        private enum Triggers
        {
            WalkingFinished,
            TimerIsEnded,
            Start
        }
    }
}