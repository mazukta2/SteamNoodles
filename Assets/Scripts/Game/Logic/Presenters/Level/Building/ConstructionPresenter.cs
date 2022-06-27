using System;
using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Repositories.Aggregations.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Services.Controls;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class ConstructionPresenter : BasePresenter<IConstructionView>
    {
        private readonly IConstructionView _constructionView;
        private readonly IDataProvider<ConstructionPresenterData> _construction;
        private readonly GhostPresentationRepository _ghost;
        private readonly GameAssetsService _assets;

        private bool _dropFinished;
        private IConstructionModelView _modelView;

        public ConstructionPresenter(IConstructionView view, IDataProvider<ConstructionPresenterData> construction)
            : this(view, construction,
                  IPresenterServices.Default?.Get<GhostPresentationRepository>(),
                  IPresenterServices.Default?.Get<GameAssetsService>(),
                  IPresenterServices.Default?.Get<GameControlsService>())
        {

        }

        public ConstructionPresenter(IConstructionView view,
            IDataProvider<ConstructionPresenterData> construction,
            GhostPresentationRepository ghost,
            GameAssetsService assets,
            GameControlsService controls) : base(view)
        {
            _constructionView = view ?? throw new ArgumentNullException(nameof(view));
            _construction = construction ?? throw new ArgumentNullException(nameof(construction));
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));
            _constructionView.Position.Value = _construction.Get().WorldPosition;
            _constructionView.Rotator.Rotation = FieldRotation.ToDirection(_construction.Get().Rotation);

            _modelView = _constructionView.Container.Spawn<IConstructionModelView>(GetPrefab());

            _modelView.Animator.OnFinished += DropFinished;
            _construction.OnRemoved += HandleRemoved;
            //_ghost.OnEvent += HandleOnEvent;
            // _ghost.OnAdded += HandleOnChanged;
            // _ghost.OnRemoved += HandleOnChanged;

            _modelView.Animator.Play(IConstructionModelView.Animations.Drop.ToString());
            controls.ShakeCamera();
            UpdateShrink();
        }


        protected override void DisposeInner()
        {
            _construction.OnRemoved -= HandleRemoved;
            //_ghost.OnEvent -= HandleOnEvent;
            // _ghost.OnAdded -= HandleOnChanged;
            // _ghost.OnRemoved -= HandleOnChanged;

            _modelView.Animator.OnFinished -= DropFinished;
            _modelView.Animator.OnFinished -= ExplosionFinished;
        }

        private IViewPrefab GetPrefab()
        {
            return _assets.GetPrefab(_construction.Get().Scheme.LevelViewPath);
        }

        private void UpdateShrink()
        {
            if (!_dropFinished)
                return;

            // if (_ghost.Has())
            // {
            //     var construction = _construction.Get();
            //     var distance = _ghost.Get().GetTargetPosition().GetDistanceTo(construction.WorldPosition);
            //     if (distance > construction.Scheme.GhostShrinkDistance)
            //         _modelView.Shrink.Value = 1;
            //     else if (distance > construction.Scheme.GhostHalfShrinkDistance)
            //         _modelView.Shrink.Value = distance / construction.Scheme.GhostShrinkDistance;
            //     else
            //         _modelView.Shrink.Value = 0.2f;
            // }
            // else
            // {
            //     _modelView.Shrink.Value = 1;
            // }
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
        
        private void HandleOnEvent(IModelEvent obj)
        {
            if (obj is not GhostMovedEvent)
                return;
            
            UpdateShrink();
        }

    }
}
