using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Controls;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class ConstructionPresenter : BasePresenter<IConstructionView>
    {
        private readonly IConstructionView _constructionView;
        private Construction _construction;
        private readonly FieldService _fieldPositionService;
        private readonly BuildingModeService _buildingModeService;
        private readonly ConstructionsService _constructionsService;
        private readonly IAssets _assets;
        private bool _dropFinished;
        private IConstructionModelView _modelView;

        public ConstructionPresenter(IConstructionView view, Construction construction)
            : this(view, construction,
                  IPresenterServices.Default?.Get<FieldService>(),
                  IPresenterServices.Default?.Get<BuildingModeService>(),
                  IPresenterServices.Default?.Get<ConstructionsService>(),
                  IGameAssets.Default,
                  IGameControls.Default)
        {

        }

        public ConstructionPresenter(IConstructionView view,
            Construction construction,
            FieldService fieldPositionService,
            BuildingModeService buildingModeService,
            ConstructionsService constructionsService,
            IAssets assets,
            IControls controls) : base(view)
        {
            _constructionView = view ?? throw new ArgumentNullException(nameof(view));
            _construction = construction ?? throw new ArgumentNullException(nameof(construction));
            _fieldPositionService = fieldPositionService ?? throw new ArgumentNullException(nameof(fieldPositionService));
            _buildingModeService = buildingModeService ?? throw new ArgumentNullException(nameof(buildingModeService));
            _constructionsService = constructionsService ?? throw new ArgumentNullException(nameof(constructionsService));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));
            _constructionView.Position.Value = _fieldPositionService.GetWorldPosition(construction);
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

            if (_buildingModeService.IsEnabled)
            {
                var distance = _buildingModeService.GetTargetPosition()
                    .GetDistanceTo(_fieldPositionService.GetWorldPosition(_construction));
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
            _constructionView.EffectsContainer.Spawn(_constructionView.ExplosionPrototype, _fieldPositionService.GetWorldPosition(_construction));
        }

        private void HandleOnUpdate()
        {
            UpdateShrink();
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
