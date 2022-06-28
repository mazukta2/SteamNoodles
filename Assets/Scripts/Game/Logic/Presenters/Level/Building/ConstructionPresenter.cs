using System;
using Game.Assets.Scripts.Game.Logic.Aggregations.ViewModels.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class ConstructionPresenter : BasePresenter<IConstructionView>
    {
        private readonly IConstructionView _constructionView;
        private readonly ConstructionViewModel _construction;

        private bool _dropFinished;
        private IConstructionVisualView _visualView;

        public ConstructionPresenter(IConstructionView view, Uid constructionId)
            : this(view,IPresenterServices.Default.Get<ConstructionViewModelRepository>().Get(constructionId))
        {

        }

        public ConstructionPresenter(IConstructionView view, ConstructionViewModel construction) : base(view)
        {
            _constructionView = view ?? throw new ArgumentNullException(nameof(view));
            _construction = construction ?? throw new ArgumentNullException(nameof(construction));
            _constructionView.Position.Value = _construction.WorldPosition;
            _constructionView.Rotator.Rotation = FieldRotation.ToDirection(_construction.Rotation);


            _construction.OnDispose += HandleRemoved;
            _construction.OnShrinkChanged += HandleOnChanged;
            
            _visualView = _constructionView.Container.Spawn<IConstructionVisualView>(construction.Prefab);
            _visualView.Animator.OnFinished += DropFinished;
            _visualView.Animator.Play(IConstructionVisualView.Animations.Drop.ToString());
            //controls.ShakeCamera();
            UpdateShrink();
        }


        protected override void DisposeInner()
        {
            _construction.OnDispose -= HandleRemoved;
            _construction.OnShrinkChanged -= HandleOnChanged;
            _visualView.Animator.OnFinished -= DropFinished;
            _visualView.Animator.OnFinished -= ExplosionFinished;
        }

        private void UpdateShrink()
        {
            if (!_dropFinished)
                return;

            _visualView.Shrink.Value = _construction.Shrink;
        }

        private void DropFinished()
        {
            _visualView.Animator.OnFinished -= DropFinished;
            _visualView.Animator.SwitchTo(IConstructionVisualView.Animations.Idle.ToString());
            _dropFinished = true;
            UpdateShrink();
        }

        private void ExplosionFinished()
        {
            _visualView.Animator.OnFinished -= ExplosionFinished;
            _visualView.Animator.SwitchTo(IConstructionVisualView.Animations.Idle.ToString());
            _constructionView.Dispose();
        }

        private void HandleExplosion()
        {
            _visualView.Animator.OnFinished += ExplosionFinished;
            _visualView.Animator.Play(IConstructionVisualView.Animations.Explode.ToString());
            _constructionView.EffectsContainer.Spawn(_constructionView.ExplosionPrototype, _constructionView.Position.Value);
        }

        private void HandleRemoved()
        {
            HandleExplosion();
        }

        private void HandleOnChanged()
        {
            UpdateShrink();
        }
    }
}
