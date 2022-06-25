using System;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class GhostManagerPresenter : BasePresenter<IGhostManagerView>
    {
        private IGhostManagerView _view;
        private readonly ISingleQuery<ConstructionGhost> _ghost;

        public GhostManagerPresenter(IGhostManagerView view) 
            : this(view,
                  IPresenterServices.Default?.Get<ISingletonRepository<ConstructionGhost>>().AsQuery())
        {
        }

        public GhostManagerPresenter(IGhostManagerView view, ISingleQuery<ConstructionGhost> ghost) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _ghost.OnAdded += CreateGhost;
            _ghost.OnRemoved += RemoveGhost;
        }

        protected override void DisposeInner()
        {
            _ghost.Dispose();
            _ghost.OnAdded -= CreateGhost;
            _ghost.OnRemoved -= RemoveGhost;
            RemoveGhost();
        }

        private void CreateGhost()
        {
            _view.Container.Spawn<IGhostView>(_view.GhostPrototype).Init();
        }

        private void RemoveGhost()
        {
            _view.Container.Clear();
        }

    }
}
