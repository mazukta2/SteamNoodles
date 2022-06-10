using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Requests.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class ConstructionPresenter : BasePresenter<IConstructionView>
    {
        private readonly IConstructionView _constructionView;
        private IConstructionModel _construction;

        //private IConstructionModelView _modelView;
        //private bool _dropFinished;
        private bool _isExploading;


        public ConstructionPresenter(IConstructionView view, Uid constructionId) 
            : this(view, 
                  IPresenterServices.Default.Get<ConstructionsRequestProviderService>().Get(constructionId))
        {

        }

        public ConstructionPresenter(IConstructionView view, IConstructionModel constructionModel) : base(view)
        {
            _constructionView = view ?? throw new ArgumentNullException(nameof(view));
            _construction = constructionModel ?? throw new ArgumentNullException(nameof(constructionModel));

            _constructionView.Position.Value = _construction.WorldPosition;
            _constructionView.Rotator.Rotation = FieldRotation.ToDirection(_construction.Rotation);

            _construction.CreateModel(_constructionView.Container);

            //_construction.Build();
            //    _modelView = _constructionView.Container.Spawn<IConstructionModelView>(assets.GetPrefab(_link.Get().Scheme.LevelViewPath));
            //  
            //_modelView.Animator.Play(IConstructionModelView.Animations.Drop.ToString());

            //_modelView.Animator.OnFinished += DropFinished;
            //_construction.OnDestroyed += HandleModelDispose;
            //_construction.OnExplostion += HandleExplosion;
            //_link.OnEvent += HandleOnEvent;

            //_ghostManager.OnGhostChanged += UpdateGhost;
            //_ghostManager.OnGhostPostionChanged += UpdateGhost;
            UpdateShrink();
        }

        protected override void DisposeInner()
        {
            _construction.Dispose();
            //_construction.OnDestroyed -= HandleModelDispose;
            //_construction.OnExplostion -= HandleExplosion;

            //_link.Dispose();
            //_link.OnDispose -= HandleModelDispose;
            //_link.OnEvent -= HandleOnEvent;
            //_modelView.Animator.OnFinished -= DropFinished;
            ////_ghostManager.OnGhostChanged -= UpdateGhost;
            ////_ghostManager.OnGhostPostionChanged -= UpdateGhost;
            //_modelView.Animator.OnFinished -= ExplosionFinished;
        }

        private void UpdateShrink()
        {
            //if (!_dropFinished)
            //    return;

            //var ghost = _ghostManager.GetGhost();
            //if (ghost != null)
            //{
            //    var distance = ghost.GetTargetPosition().GetDistanceTo(_fieldPositionService.GetWorldPosition(_link.Get()));
            //    if (distance > _constrcutionsSettings.GhostShrinkDistance)
            //        _modelView.Shrink.Value = 1;
            //    else if (distance > _constrcutionsSettings.GhostHalfShrinkDistance)
            //        _modelView.Shrink.Value = distance / _constrcutionsSettings.GhostShrinkDistance;
            //    else
            //        _modelView.Shrink.Value = 0.2f;
            //}
            //else
            //{
            //    _modelView.Shrink.Value = 1;
            //}
        }

        private void DropFinished()
        {
            //_modelView.Animator.OnFinished -= DropFinished;
            //_modelView.Animator.SwitchTo(IConstructionModelView.Animations.Idle.ToString());
            //_dropFinished = true;
            UpdateShrink();
        }

        private void ExplosionFinished()
        {
            //_modelView.Animator.OnFinished -= ExplosionFinished;
            //_modelView.Animator.SwitchTo(IConstructionModelView.Animations.Idle.ToString());
            //_isExploading = false;
            //if (_link.IsDisposed)
            //    _constructionView.Dispose();
        }

        private void HandleExplosion()
        {
            //_modelView.Animator.OnFinished += ExplosionFinished;
            _isExploading = true;
            //_modelView.Animator.Play(IConstructionModelView.Animations.Explode.ToString());
            //_constructionView.EffectsContainer.Spawn(_constructionView.ExplosionPrototype, _fieldPositionService.GetWorldPosition(_link.Get()));
        }

        private void HandleModelDispose()
        {
            if (_isExploading)
                return;

            _constructionView.Dispose();
        }

    }
}
