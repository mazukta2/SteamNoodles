using System;
using Game.Assets.Scripts.Game.Logic.Aggregations.Constructions;
using Game.Assets.Scripts.Game.Logic.Aggregations.Constructions.Ghosts;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Repositories.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Assets;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class GhostPresenter : BasePresenter<IGhostView>
    {
        private readonly IGhostView _view;
        private readonly Ghost _ghost;
        private readonly ConstructionsRepository _constructions;
        private readonly GameAssetsService _assets;
        private readonly IConstructionModelView _constructionModelView;

        public GhostPresenter(IGhostView view) : this(view, 
            IPresenterServices.Default?.Get<GhostRepository>().Get(),
            IPresenterServices.Default?.Get<ConstructionsRepository>(),
            IPresenterServices.Default?.Get<GameAssetsService>())
        {

        }

        public GhostPresenter(IGhostView view,
            Ghost ghost,
            ConstructionsRepository constructions,
            GameAssetsService assets) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));

            _view.Container.Clear();
            var modelView = _view.Container.Spawn<IConstructionModelView>(_ghost.GetPrefab());
            _constructionModelView = modelView;
            _constructionModelView.Animator.Play(IConstructionModelView.Animations.Dragging.ToString());
            
            _ghost.OnDispose += Dispose;
            //_ghost.OnEvent += HandleEvent;

            UpdatePosition();
        }

        protected override void DisposeInner()
        {
            _ghost.OnDispose -= Dispose;
            //_ghost.OnEvent -= HandleEvent;
        }

        private void HandleEvent(IModelEvent obj)
        {
            if (obj is not GhostMovedEvent)
                return;
            
            UpdatePosition();
        }
        
        private void UpdatePosition()
        {
            _view.LocalPosition.Value = _ghost.GetWorldPosition();
            _view.Rotator.Rotation = FieldRotation.ToDirection(_ghost.GetRotation());
            
            _constructionModelView.BorderAnimator.Play(_ghost.CanBuild() ? IConstructionModelView.BorderAnimations.Idle.ToString() : IConstructionModelView.BorderAnimations.Disallowed.ToString());
        }

    }
}
