using System;
using Game.Assets.Scripts.Game.Logic.Aggregations.Building;
using Game.Assets.Scripts.Game.Logic.Aggregations.Fields;
using Game.Assets.Scripts.Game.Logic.Aggregations.ViewModels.Ghosts;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Services.Assets;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class GhostPresenter : BasePresenter<IGhostView>
    {
        private readonly IGhostView _view;
        private readonly GhostViewModel _ghost;
        private readonly FieldConstructionsRepository _fieldConstructions;
        private readonly GameAssetsService _assets;
        private readonly IConstructionVisualView _constructionVisualView;

        public GhostPresenter(IGhostView view) : this(view, 
            IPresenterServices.Default?.Get<GhostViewModelRepository>().Get())
        {

        }

        public GhostPresenter(IGhostView view, GhostViewModel ghost) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));

            _view.Container.Clear();
            var modelView = _view.Container.Spawn<IConstructionVisualView>(_ghost.Prefab);
            _constructionVisualView = modelView;
            _constructionVisualView.Animator.Play(IConstructionVisualView.Animations.Dragging.ToString());
            
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
            _view.LocalPosition.Value = _ghost.WorldPosition;
            _view.Rotator.Rotation = FieldRotation.ToDirection(_ghost.Rotation);
            
            _constructionVisualView.BorderAnimator.Play(_ghost.CanBuild ? IConstructionVisualView.BorderAnimations.Idle.ToString() : IConstructionVisualView.BorderAnimations.Disallowed.ToString());
        }

    }
}
