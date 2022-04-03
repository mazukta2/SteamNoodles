using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level.Units;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Units
{
    public class UnitPresenter : BasePresenter
    {
        public Unit Unit => _model;

        private UnitView _view;
        private Unit _model;

        public UnitPresenter(Unit model, UnitView view) : base(view)
        {
            _view = view;
            _view.Position.Value = model.Position;
            _model = model;
            _view.Rotator.FaceTo(model.Target);
            _model.OnPositionChanged += HandleOnPositionChanged;
            _model.OnDispose += HandleOnDispose;
            _model.OnTargetChanged += HandleOnTargetChanged;
            _model.OnReachedPosition += HandleOnReachedPosition;

            PlayAnimation(model.IsMoving() ? Animations.Run : Animations.Idle);
        }

        protected override void DisposeInner()
        {
            _model.OnPositionChanged -= HandleOnPositionChanged;
            _model.OnDispose -= HandleOnDispose;
            _model.OnTargetChanged -= HandleOnTargetChanged;
            _model.OnReachedPosition -= HandleOnReachedPosition;
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

        private void PlayAnimation(Animations animations)
        {
            _view.Animator.Play(animations.ToString());
        }

        public enum Animations
        {
            Idle,
            Run
        }
    }
}
