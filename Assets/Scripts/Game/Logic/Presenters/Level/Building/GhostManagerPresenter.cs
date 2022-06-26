using System;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class GhostManagerPresenter : BasePresenter<IGhostManagerView>
    {
        private IGhostManagerView _view;
        private readonly IDataProvider<GhostData> _ghost;

        public GhostManagerPresenter(IGhostManagerView view) 
            : this(view,
                  IPresenterServices.Default?.Get<IDataProviderService<GhostData>>().Get())
        {
        }

        public GhostManagerPresenter(IGhostManagerView view, IDataProvider<GhostData> ghost) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _ghost.OnAdded += CreateGhost;
            _ghost.OnRemoved += RemoveGhost;
        }

        protected override void DisposeInner()
        {
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
