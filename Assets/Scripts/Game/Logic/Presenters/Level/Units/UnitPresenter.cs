using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Common;
using System;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Units;
using Game.Assets.Scripts.Game.Logic.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Events.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Animations;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Units;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Units
{
    public class UnitPresenter : BasePresenter<IUnitView>
    {
        private IUnitView _view;
        private readonly IDataProvider<UnitData> _unit;
        private UnitRotator _rotator;
        private bool _isStartingAnimation = false;

        public UnitPresenter(IUnitView view, IDataProvider<UnitData> unit, IGameTime time) : base(view)
        {
            _view = view;
            _unit = unit;
            
            var unitData = _unit.Get();
            _rotator = new UnitRotator(view.Rotator, time, 0.1f, unitData.UnitType.RotationSpeed);

            _view.Position.Value = unitData.Position;
            if (unitData.Target != unitData.Position)
                _rotator.Direction = (unitData.Target - unitData.Position).ToQuaternion();
            else
                _rotator.Direction = new GameVector3(-1, 0, 0).ToQuaternion();

            _rotator.Skip();
            _unit.OnEvent += HandleEvent;
            _unit.OnRemoved += HandleOnDispose;

            DressUnit();
            _view.Animator.OnFinished += Animator_OnFinished;
            _isStartingAnimation = true;
            PlayAnimation(Animations.Start);
            UpdateAnimations();
        }

        protected override void DisposeInner()
        {
            _rotator.Dispose();
            _view.Animator.OnFinished -= Animator_OnFinished;
            _unit.OnEvent -= HandleEvent;
            _unit.OnRemoved -= HandleOnDispose;
        }

        private void HandleEvent(IModelEvent evnt)
        {
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
            var unit = _unit.Get();
            _view.Position.Value = unit.Position;
            UpdateAnimations();
            if (unit.IsMoving)
                _view.Animator.SetSpeed(unit.CurrentSpeed / unit.MaxSpeed);
            else
                _view.Animator.SetSpeed(1);
        }

        private void HandleOnTargetChanged()
        {
            var unit = _unit.Get();
            if (unit.Target != unit.Position)
                _rotator.Direction = (unit.Target - unit.Position).ToQuaternion();
            UpdateAnimations();
        }

        private void HandleOnDispose()
        {
            _view.Dispose();
        }

        private void HandleOnReachedPosition()
        {
            UpdateAnimations();
        }

        private void HandleOnLookAt(GameVector3 target, bool skip)
        {
            _rotator.Direction = (target - _unit.Get().Position).ToQuaternion();
            if (skip)
                _rotator.Skip();
        }

        private void UpdateAnimations()
        {
            if (_isStartingAnimation)
                return;

            PlayAnimation(_unit.Get().IsMoving ? Animations.Run : Animations.Idle);
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

            var hair = _unit.Get().Hair;
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
