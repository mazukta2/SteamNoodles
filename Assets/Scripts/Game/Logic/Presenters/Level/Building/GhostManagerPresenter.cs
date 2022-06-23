using System;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class GhostManagerPresenter : BasePresenter<IGhostManagerView>
    {
        private IGhostManagerView _view;
        private readonly GhostService _ghostService;

        public GhostManagerPresenter(IGhostManagerView view) 
            : this(view,
                  IPresenterServices.Default?.Get<GhostService>())
        {
        }

        public GhostManagerPresenter(IGhostManagerView view, GhostService ghostService) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghostService = ghostService ?? throw new ArgumentNullException(nameof(ghostService));
            _ghostService.OnShowed += CreateGhost;
            _ghostService.OnHided += RemoveGhost;
        }

        protected override void DisposeInner()
        {
            _ghostService.OnShowed -= CreateGhost;
            _ghostService.OnHided -= RemoveGhost;
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
