using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level.Units;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Units
{
    public class UnitPresenter : BasePresenter<IUnitView>
    {
        //public Unit Unit => _model;

        private IUnitView _view;
        private PresenterModel<Unit> _model;
        private UnitsSettingsDefinition _settings;
        private UnitRotator _rotator;
        private bool _isStartingAnimation = false;

        public UnitPresenter(EntityLink<Unit> link, IUnitView view, UnitsSettingsDefinition unitsSettingsDefinition, IGameTime time) : base(view)
        {
            _view = view;
            _model = link.CreateModel();
            _settings = unitsSettingsDefinition ?? throw new ArgumentNullException(nameof(unitsSettingsDefinition));
            _rotator = new UnitRotator(view.Rotator, time, 0.1f, unitsSettingsDefinition.RotationSpeed);

            _view.Position.Value = _model.Value.Position;
            if (_model.Value.Target != _model.Value.Position)
                _rotator.Direction = (_model.Value.Target - _model.Value.Position).ToQuaternion();
            _rotator.Skip();
            _model.OnEvent += HandleEvent;
            _model.OnDispose += HandleOnDispose;

            DressUnit();
            _view.Animator.OnFinished += Animator_OnFinished;
            _isStartingAnimation = true;
            PlayAnimation(Animations.Start);
            UpdateAnimations();
        }

        protected override void DisposeInner()
        {
            _model.Dispose();
            _rotator.Dispose();
            _view.Animator.OnFinished -= Animator_OnFinished;
            _model.OnEvent -= HandleEvent;
            _model.OnDispose -= HandleOnDispose;
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
            _view.Position.Value = _model.Value.Position;
            UpdateAnimations();
            if (_model.Value.IsMoving())
                _view.Animator.SetSpeed(_model.Value.CurrentSpeed / _model.Value.GetMaxSpeed());
            else
                _view.Animator.SetSpeed(1);
        }

        private void HandleOnTargetChanged()
        {
            if (_model.Value.Target != _model.Value.Position)
                _rotator.Direction = (_model.Value.Target - _model.Value.Position).ToQuaternion();
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

        private void HandleOnLookAt(Common.Math.GameVector3 target, bool skip)
        {
            _rotator.Direction = (target - _model.Value.Position).ToQuaternion();
            if (skip)
                _rotator.Skip();
        }

        private void UpdateAnimations()
        {
            if (_isStartingAnimation)
                return;

            PlayAnimation(_model.Value.IsMoving() ? Animations.Run : Animations.Idle);
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
            var random = new Random(_model.Value.VisualSeed);
            var hair = _settings.Hairs.GetRandom(random);

            _view.UnitDresser.Clear();
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
