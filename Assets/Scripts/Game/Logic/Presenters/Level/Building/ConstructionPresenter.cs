using System;
using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class ConstructionPresenter : BasePresenter<IConstructionView>
    {
        private readonly IConstructionView _constructionView;
        private readonly Construction _construction;
        private readonly GhostService _ghostService;
        private readonly ConstructionsService _constructionsService;
        private readonly IRepository<Construction> _constructions;
        private readonly GameAssetsService _assets;

        private bool _dropFinished;
        private IConstructionModelView _modelView;

        public ConstructionPresenter(IConstructionView view, Construction construction)
            : this(view, construction,
                  IPresenterServices.Default?.Get<GhostService>(),
                  IPresenterServices.Default?.Get<ConstructionsService>(),
                  IPresenterServices.Default?.Get<IRepository<Construction>>(),
                  IPresenterServices.Default?.Get<GameAssetsService>(),
                  IPresenterServices.Default?.Get<GameControlsService>())
        {

        }

        public ConstructionPresenter(IConstructionView view,
            Construction construction,
            GhostService ghostService,
            ConstructionsService constructionsService,
            IRepository<Construction> constructions,
            GameAssetsService assets,
            GameControlsService controls) : base(view)
        {
            _constructionView = view ?? throw new ArgumentNullException(nameof(view));
            _construction = construction ?? throw new ArgumentNullException(nameof(construction));
            _ghostService = ghostService ?? throw new ArgumentNullException(nameof(ghostService));
            _constructionsService = constructionsService ?? throw new ArgumentNullException(nameof(constructionsService));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));
            _constructionView.Position.Value = _constructionsService.GetWorldPosition(construction);
            _constructionView.Rotator.Rotation = FieldRotation.ToDirection(construction.Rotation);

            _modelView = _constructionView.Container.Spawn<IConstructionModelView>(GetPrefab());

            _modelView.Animator.OnFinished += DropFinished;
            _constructions.OnRemoved += HandleRemoved;
            _ghostService.OnChanged += HandleOnChanged;
            _ghostService.OnShowed += HandleOnChanged;
            _ghostService.OnHided += HandleOnChanged;

            _modelView.Animator.Play(IConstructionModelView.Animations.Drop.ToString());
            controls.ShakeCamera();
            UpdateShrink();
        }


        protected override void DisposeInner()
        {
            _constructions.OnRemoved -= HandleRemoved;
            _ghostService.OnChanged -= HandleOnChanged;
            _ghostService.OnShowed -= HandleOnChanged;
            _ghostService.OnHided -= HandleOnChanged;

            _modelView.Animator.OnFinished -= DropFinished;
            _modelView.Animator.OnFinished -= ExplosionFinished;
        }

        private IViewPrefab GetPrefab()
        {
            return _assets.GetPrefab(_construction.Scheme.LevelViewPath);
        }

        private void UpdateShrink()
        {
            if (!_dropFinished)
                return;

            if (_ghostService.IsEnabled())
            {
                var distance = _ghostService.GetGhost().TargetPosition
                    .GetDistanceTo(_constructionsService.GetWorldPosition(_construction));
                if (distance > _construction.Scheme.GhostShrinkDistance)
                    _modelView.Shrink.Value = 1;
                else if (distance > _construction.Scheme.GhostHalfShrinkDistance)
                    _modelView.Shrink.Value = distance / _construction.Scheme.GhostShrinkDistance;
                else
                    _modelView.Shrink.Value = 0.2f;
            }
            else
            {
                _modelView.Shrink.Value = 1;
            }
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
            _constructionView.EffectsContainer.Spawn(_constructionView.ExplosionPrototype, 
                _constructionsService.GetWorldPosition(_construction));
        }

        private void HandleRemoved(Construction obj)
        {
            if (obj.Id == _construction.Id)
                HandleExplosion();
        }

        private void HandleOnChanged()
        {
            UpdateShrink();
        }
    }
}
