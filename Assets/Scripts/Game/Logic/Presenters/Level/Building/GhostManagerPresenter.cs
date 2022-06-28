using System;
using Game.Assets.Scripts.Game.Logic.Aggregations.Constructions.Ghosts;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Repositories.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class GhostManagerPresenter : BasePresenter<IGhostManagerView>
    {
        private readonly IGhostManagerView _view;
        private readonly GhostRepository _ghost;

        public GhostManagerPresenter(IGhostManagerView view) 
            : this(view,
                  IPresenterServices.Default?.Get<GhostRepository>())
        {
        }

        public GhostManagerPresenter(IGhostManagerView view, GhostRepository ghost) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            // _ghost.OnAdded += CreateGhost;
            // _ghost.OnRemoved += RemoveGhost;
        }

        protected override void DisposeInner()
        {
            // _ghost.OnAdded -= CreateGhost;
            // _ghost.OnRemoved -= RemoveGhost;
            RemoveGhost();
        }

        private void CreateGhost()
        {
            // _view.Container.Spawn<IGhostView>(_view.GhostPrototype).Init(_ghost.Get());
        }

        private void RemoveGhost()
        {
            _view.Container.Clear();
        }

    }
}
