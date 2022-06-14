using System;
using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Levels;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class ConstructionPresenter : BasePresenter<IConstructionView>
    {
        private readonly IConstructionView _constructionView;
        private readonly Construction _construction;
        private readonly BuildingModeService _buildingModeService;
        private readonly ConstructionsService _constructionsService;
        private readonly GameAssetsService _assets;

        private bool _dropFinished;
        private IConstructionModelView _modelView;

        public ConstructionPresenter(IConstructionView view, Construction construction)
            : this(view, construction,
                  IPresenterServices.Default?.Get<BuildingModeService>(),
                  IPresenterServices.Default?.Get<ConstructionsService>(),
                  IPresenterServices.Default?.Get<GameAssetsService>(),
                  IPresenterServices.Default?.Get<GameControlsService>())
        {

        }

        public ConstructionPresenter(IConstructionView view,
            Construction construction,
            BuildingModeService buildingModeService,
            ConstructionsService constructionsService,
            GameAssetsService assets,
            GameControlsService controls) : base(view)
        {
            _constructionView = view ?? throw new ArgumentNullException(nameof(view));
            _construction = construction ?? throw new ArgumentNullException(nameof(construction));
            _buildingModeService = buildingModeService ?? throw new ArgumentNullException(nameof(buildingModeService));
            _constructionsService = constructionsService ?? throw new ArgumentNullException(nameof(constructionsService));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));
            _constructionView.Position.Value = _constructionsService.GetWorldPosition(construction);
            _constructionView.Rotator.Rotation = FieldRotation.ToDirection(construction.Rotation);

            _modelView = _constructionView.Container.Spawn<IConstructionModelView>(GetPrefab());

            _modelView.Animator.OnFinished += DropFinished;
            _constructionsService.OnRemoved += HandleRemoved;
            _buildingModeService.OnChanged += HandleOnChanged;
            _buildingModeService.OnPositionChanged += HandleOnPositionChanged;

            _modelView.Animator.Play(IConstructionModelView.Animations.Drop.ToString());
            controls.ShakeCamera();
            UpdateShrink();
        }


        protected override void DisposeInner()
        {
            _constructionsService.OnRemoved -= HandleRemoved;
            _buildingModeService.OnChanged -= HandleOnChanged;
            _buildingModeService.OnPositionChanged -= HandleOnPositionChanged;

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

            if (_buildingModeService.IsEnabled())
            {
                var distance = _buildingModeService.GetTargetPosition()
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

        private void HandleOnPositionChanged()
        {
            UpdateShrink();
        }

        private void HandleOnChanged(bool obj)
        {
            UpdateShrink();
        }
    }
}
