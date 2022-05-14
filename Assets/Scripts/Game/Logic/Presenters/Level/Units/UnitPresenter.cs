using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Units;
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

        public UnitPresenter(Unit model, IUnitView view, UnitsSettingsDefinition unitsSettingsDefinition) : base(view)
        {
            _view = view;
            _model = model;
            _settings = unitsSettingsDefinition ?? throw new ArgumentNullException(nameof(unitsSettingsDefinition));

            _view.Position.Value = model.Position;
            _view.Rotator.FaceTo(model.Target);
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
        }

        private void HandleOnTargetChanged()
        {
            _view.Rotator.FaceTo(_model.Target);
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
            _view.Rotator.FaceTo(obj);
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
