using Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class ConstructionPresenter : BasePresenter<IConstructionView>
    {
        private readonly IConstructionView _constructionView;
        private IConstructionModel _construction;
        private bool _dropFinished;
        private IConstructionModelView _modelView;

        public ConstructionPresenter(IConstructionView view, Uid constructionId) 
            : this(view, 
                  IPresenterServices.Default.Get<ConstructionsRequestsService>().Get(constructionId))
        {

        }

        public ConstructionPresenter(IConstructionView view, 
            IConstructionModel constructionModel) : base(view)
        {
            _constructionView = view ?? throw new ArgumentNullException(nameof(view));
            _construction = constructionModel ?? throw new ArgumentNullException(nameof(constructionModel));

            _constructionView.Position.Value = _construction.GetWorldPosition();
            _constructionView.Rotator.Rotation = FieldRotation.ToDirection(_construction.GetRotation());

            _modelView = _constructionView.Container.Spawn<IConstructionModelView>(_construction.GetModelAsset());

            _modelView.Animator.OnFinished += DropFinished;
            _construction.OnExplostion += HandleExplosion;
            _construction.OnUpdate += HandleOnUpdate;

            _modelView.Animator.Play(IConstructionModelView.Animations.Drop.ToString());
            _construction.Shake();
            UpdateShrink();
        }

        protected override void DisposeInner()
        {
            _construction.Dispose();
            _construction.OnExplostion -= HandleExplosion;
            _construction.OnUpdate -= HandleOnUpdate;

            _modelView.Animator.OnFinished -= DropFinished;
            _modelView.Animator.OnFinished -= ExplosionFinished;
        }

        private void UpdateShrink()
        {
            if (!_dropFinished)
                return;

            _modelView.Shrink.Value = _construction.GetShrinkValue();
        }

        private void DropFinished()
        {
            _modelView.Animator.OnFinished -= DropFinished;
            _modelView.Animator.SwitchTo(IConstructionModelView.Animations.Idle.ToString());
            _dropFinished = true;
            UpdateShrink();
        }

        private void ExplosionFinished()
        {
            _modelView.Animator.OnFinished -= ExplosionFinished;
            _modelView.Animator.SwitchTo(IConstructionModelView.Animations.Idle.ToString());
            _constructionView.Dispose();
        }

        private void HandleExplosion()
        {
            _modelView.Animator.OnFinished += ExplosionFinished;
            _modelView.Animator.Play(IConstructionModelView.Animations.Explode.ToString());
            _constructionView.EffectsContainer.Spawn(_constructionView.ExplosionPrototype, _construction.GetWorldPosition());
        }

        private void HandleOnUpdate()
        {
            UpdateShrink();
        }

    }
}
