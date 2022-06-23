using System;
using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
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
        private readonly Field _Field;
        private readonly GameAssetsService _assets;

        public GhostPresenter(IGhostView view) : this(view,
            IPresenterServices.Default.Get<GhostService>(),
            IPresenterServices.Default.Get<ISingletonRepository<Field>>().Get(),
            IPresenterServices.Default.Get<GameAssetsService>())
        {

        }

        public GhostPresenter(IGhostView view,
            GhostService ghostService,
            Field Field,
            GameAssetsService assets) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghostService = ghostService ?? throw new ArgumentNullException(nameof(ghostService));
            _Field = Field ?? throw new ArgumentNullException(nameof(Field));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));

            if (!_ghostService.IsEnabled()) throw new Exception("Ghost can exist only in building mode");


            _view.Container.Clear();
            var modelView = _view.Container.Spawn<IConstructionModelView>(GetPrefab());
            modelView.Animator.Play(IConstructionModelView.Animations.Dragging.ToString());

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
            var worldPosition = _Field.GetWorldPosition(ghost.Position, size);
            _view.LocalPosition.Value = worldPosition;
            _view.Rotator.Rotation = FieldRotation.ToDirection(ghost.Rotation);
        }

    }
}
