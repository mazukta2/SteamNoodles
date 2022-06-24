using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Common;
using System;
using Game.Assets.Scripts.Game.Logic.Models.Events.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Animations;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Units;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Units
{
    public class UnitPresenter : BasePresenter<IUnitView>
    {
        private IUnitView _view;
        private Unit _unit;
        private readonly IQuery<Unit> _units;
        private UnitRotator _rotator;
        private bool _isStartingAnimation = false;

        public UnitPresenter(IUnitView view, Unit unit, IQuery<Unit> units, IGameTime time) : base(view)
        {
            _view = view;
            _unit = unit;
            _units = units;
            _rotator = new UnitRotator(view.Rotator, time, 0.1f, unit.UnitType.RotationSpeed);

            _view.Position.Value = unit.Position;
            if (unit.Target != unit.Position)
                _rotator.Direction = (unit.Target - unit.Position).ToQuaternion();
            else
                _rotator.Direction = new GameVector3(-1, 0, 0).ToQuaternion();

            _rotator.Skip();
            _units.OnEvent += HandleEvent;
            _units.OnRemoved += HandleOnDispose;

            DressUnit();
            _view.Animator.OnFinished += Animator_OnFinished;
            _isStartingAnimation = true;
            PlayAnimation(Animations.Start);
            UpdateAnimations();
        }

        protected override void DisposeInner()
        {
            _rotator.Dispose();
            _units.Dispose();
            _view.Animator.OnFinished -= Animator_OnFinished;
            _units.OnEvent -= HandleEvent;
            _units.OnRemoved -= HandleOnDispose;
        }

        private void HandleEvent(Unit unit, IModelEvent evnt)
        {
            if (unit.Id != _unit.Id)
                return;

            _unit = unit;

            if (evnt is UnitSmokeEvent) HandleOnSmoke();
            if (evnt is UnitPositionChangedEvent) HandleOnPositionChanged();
            if (evnt is UnitReachedTargetPositionEvent) HandleOnReachedPosition();
            if (evnt is UnitLookAtEvent lookEvnt) HandleOnLookAt(lookEvnt.Target, lookEvnt.Skip);
            if (evnt is UnitTargetChangedEvent) HandleOnTargetChanged();
        }

        private void HandleOnSmoke()
        {
            _view.SmokeContainer.Spawn(_view.SmokePrefab);
        }

        private void HandleOnPositionChanged()
        {
            _view.Position.Value = _unit.Position;
            UpdateAnimations();
            if (_unit.IsMoving())
                _view.Animator.SetSpeed(_unit.CurrentSpeed / _unit.GetMaxSpeed());
            else
                _view.Animator.SetSpeed(1);
        }

        private void HandleOnTargetChanged()
        {
            if (_unit.Target != _unit.Position)
                _rotator.Direction = (_unit.Target - _unit.Position).ToQuaternion();
            UpdateAnimations();
        }

        private void HandleOnDispose(Unit unit)
        {
            if (unit.Id != _unit.Id)
                return;

            _view.Dispose();
        }

        private void HandleOnReachedPosition()
        {
            UpdateAnimations();
        }

        private void HandleOnLookAt(Common.Math.GameVector3 target, bool skip)
        {
            _rotator.Direction = (target - _unit.Position).ToQuaternion();
            if (skip)
                _rotator.Skip();
        }

        private void UpdateAnimations()
        {
            if (_isStartingAnimation)
                return;

            PlayAnimation(_unit.IsMoving() ? Animations.Run : Animations.Idle);
        }

        private void Animator_OnFinished()
        {
            _isStartingAnimation = false;
            UpdateAnimations();
        }

        private void PlayAnimation(Animations animations)
        {
            _view.Animator.Play(animations.ToString());
        }

        private void DressUnit()
        {
            _view.UnitDresser.Clear();

            var hair = _unit.GetHair();
            if (!string.IsNullOrEmpty(hair))
                _view.UnitDresser.SetHair(hair);
        }

        public enum Animations
        {
            Idle,
            Run,
            Start
        }
    }
}
