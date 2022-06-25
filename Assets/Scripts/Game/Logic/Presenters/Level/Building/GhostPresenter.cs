using System;
using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Functions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Services.Assets;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class GhostPresenter : BasePresenter<IGhostView>
    {
        private readonly IGhostView _view;
        private readonly ISingleQuery<ConstructionGhost> _ghost;
        private readonly IQuery<Construction> _constructions;
        private readonly GameAssetsService _assets;
        private readonly IConstructionModelView _constructionModelView;

        public GhostPresenter(IGhostView view) : this(view,
            IPresenterServices.Default?.Get<ISingletonRepository<ConstructionGhost>>().AsQuery(),
            IPresenterServices.Default?.Get<IRepository<Construction>>().AsQuery(),
            IPresenterServices.Default?.Get<GameAssetsService>())
        {

        }

        public GhostPresenter(IGhostView view,
            ISingleQuery<ConstructionGhost> ghost,
            IQuery<Construction> constructions,
            GameAssetsService assets) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));

            if (!_ghost.Has()) throw new Exception("Ghost can exist only in building mode");

            _view.Container.Clear();
            var modelView = _view.Container.Spawn<IConstructionModelView>(GetPrefab());
            _constructionModelView = modelView;
            _constructionModelView.Animator.Play(IConstructionModelView.Animations.Dragging.ToString());
            
            _ghost.OnRemoved += Dispose;
            _ghost.OnEvent += HandleEvent;

            UpdatePosition();
        }

        protected override void DisposeInner()
        {
            _constructions.Dispose();
            _ghost.Dispose();
            _ghost.OnRemoved -= Dispose;
            _ghost.OnEvent -= HandleEvent;
        }

        private IViewPrefab GetPrefab()
        {
            return _assets.GetPrefab(_ghost.Get().Card.Scheme.LevelViewPath);
        }
        
        private void HandleEvent(IModelEvent obj)
        {
            if (obj is not GhostMovedEvent)
                return;
            
            UpdatePosition();
        }
        
        private void UpdatePosition()
        {
            var ghost = _ghost.Get();
            var size = ghost.Card.Scheme.Placement.GetRect(ghost.Rotation);
            var worldPosition = ghost.Position.GetWorldPosition(size);
            _view.LocalPosition.Value = worldPosition;
            _view.Rotator.Rotation = FieldRotation.ToDirection(ghost.Rotation);
            
            var canPlace = _ghost.Get().CanPlace();
            _constructionModelView.BorderAnimator.Play(canPlace ? IConstructionModelView.BorderAnimations.Idle.ToString() : IConstructionModelView.BorderAnimations.Disallowed.ToString());
        }

    }
}
