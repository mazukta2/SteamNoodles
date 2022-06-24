using System;
using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Functions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class GhostPresenter : BasePresenter<IGhostView>
    {
        private readonly IGhostView _view;
        private readonly GhostService _ghostService;
        private readonly IRepository<Construction> _constructions;
        private readonly GameAssetsService _assets;
        private readonly IConstructionModelView _constructionModelView;

        public GhostPresenter(IGhostView view) : this(view,
            IPresenterServices.Default.Get<GhostService>(),
            IPresenterServices.Default.Get<IRepository<Construction>>(),
            IPresenterServices.Default.Get<GameAssetsService>())
        {

        }

        public GhostPresenter(IGhostView view,
            GhostService ghostService,
            IRepository<Construction> constructions,
            GameAssetsService assets) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghostService = ghostService ?? throw new ArgumentNullException(nameof(ghostService));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));

            if (!_ghostService.IsEnabled()) throw new Exception("Ghost can exist only in building mode");


            _view.Container.Clear();
            var modelView = _view.Container.Spawn<IConstructionModelView>(GetPrefab());
            _constructionModelView = modelView;
            _constructionModelView.Animator.Play(IConstructionModelView.Animations.Dragging.ToString());
            
            _ghostService.OnHided += Dispose;
            _ghostService.OnChanged += HandleOnPositionChanged;

            HandleOnPositionChanged();
        }

        protected override void DisposeInner()
        {
            _ghostService.OnHided -= Dispose;
            _ghostService.OnChanged -= HandleOnPositionChanged;
        }

        private IViewPrefab GetPrefab()
        {
            return _assets.GetPrefab(_ghostService.GetGhost().Card.Scheme.LevelViewPath);
        }
        
        private void HandleOnPositionChanged()
        {
            var ghost = _ghostService.GetGhost();
            var size = ghost.Card.Scheme.Placement.GetRect(ghost.Rotation);
            var worldPosition = ghost.Position.GetWorldPosition(size);
            _view.LocalPosition.Value = worldPosition;
            _view.Rotator.Rotation = FieldRotation.ToDirection(ghost.Rotation);
            
            var canPlace = _constructions.CanPlace(ghost.Card, ghost.Position, ghost.Rotation);
            _constructionModelView.BorderAnimator.Play(canPlace ? IConstructionModelView.BorderAnimations.Idle.ToString() : IConstructionModelView.BorderAnimations.Disallowed.ToString());
        }

    }
}
