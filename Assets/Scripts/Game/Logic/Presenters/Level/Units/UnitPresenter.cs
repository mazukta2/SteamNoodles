using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level.Units;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Units
{
    public class UnitPresenter : BasePresenter<IUnitView>
    {
        public Unit Unit => _model;

        private IUnitView _view;
        private Unit _model;
        private UnitsSettingsDefinition _settings;
        private UnitRotator _rotator;

        public UnitPresenter(Unit model, IUnitView view, UnitsSettingsDefinition unitsSettingsDefinition, IGameTime time) : base(view)
        {
            _view = view;
            _model = model;
            _settings = unitsSettingsDefinition ?? throw new ArgumentNullException(nameof(unitsSettingsDefinition));
            _rotator = new UnitRotator(view.Rotator, time, 0.1f, unitsSettingsDefinition.RotationSpeed);

            _view.Position.Value = model.Position;
            _rotator.Direction = model.Target - model.Position;
            _rotator.Skip();
            _model.OnPositionChanged += HandleOnPositionChanged;
            _model.OnDispose += HandleOnDispose;
            _model.OnTargetChanged += HandleOnTargetChanged;
            _model.OnReachedPosition += HandleOnReachedPosition;
            _model.OnLookAt += HandleOnLookAt;

            DressUnit();
            PlayAnimation(model.IsMoving() ? Animations.Run : Animations.Idle);
        }

        protected override void DisposeInner()
        {
            _rotator.Dispose();
            _model.OnPositionChanged -= HandleOnPositionChanged;
            _model.OnDispose -= HandleOnDispose;
            _model.OnTargetChanged -= HandleOnTargetChanged;
            _model.OnReachedPosition -= HandleOnReachedPosition;
            _model.OnLookAt -= HandleOnLookAt;
        }

        private void HandleOnPositionChanged()
        {
            _view.Position.Value = _model.Position;
            PlayAnimation(_model.IsMoving() ? Animations.Run : Animations.Idle);
            if (_model.IsMoving())
                _view.Animator.SetSpeed(_model.GetCurrentSpeed() / _model.GetMaxSpeed());
            else
                _view.Animator.SetSpeed(1);
        }

        private void HandleOnTargetChanged()
        {
            _rotator.Direction = _model.Target - _model.Position;
            PlayAnimation(_model.IsMoving() ? Animations.Run : Animations.Idle);
        }

        private void HandleOnDispose()
        {
            _view.Dispose();
        }

        private void HandleOnReachedPosition()
        {
            PlayAnimation(_model.IsMoving() ? Animations.Run : Animations.Idle);
        }

        private void HandleOnLookAt(Common.Math.FloatPoint3D obj)
        {
            _rotator.Direction = obj - _model.Position;
        }

        private void PlayAnimation(Animations animations)
        {
            _view.Animator.Play(animations.ToString());
        }

        private void DressUnit()
        {
            var random = new Random(_model.VisualSeed);
            var hair = _settings.Hairs.GetRandom(random);

            _view.UnitDresser.Clear();
            _view.UnitDresser.SetHair(hair);
        }

        public enum Animations
        {
            Idle,
            Run
        }
    }
}
