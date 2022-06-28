using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using Game.Assets.Scripts.Game.Logic.Aggregations.Building;
using Game.Assets.Scripts.Game.Logic.Aggregations.ViewModels.Ghosts;
using Game.Assets.Scripts.Game.Logic.Services.Controls;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class BuildScreenPresenter : BasePresenter<IBuildScreenView>
    {
        private KeyCommand _exitKey;

        private IBuildScreenView _view;
        private readonly BuildingGhost _buildingGhost;
        private readonly GhostRepository _ghostRepository;
        private readonly ScreenService _screenService;

        public BuildScreenPresenter(IBuildScreenView view, Uid constructionCardId) : this(
                view, constructionCardId,
                IPresenterServices.Default?.Get<GhostRepository>(),
                IPresenterServices.Default?.Get<ScreenService>(),
                IPresenterServices.Default?.Get<GameControlsService>())
        {
        }

        public BuildScreenPresenter(IBuildScreenView view, 
            Uid cardId, GhostViewModel ghost,
            ScreenService screenService,
            GameControlsService gameKeysManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));

            _ghostRepository = ghostCommands ?? throw new ArgumentNullException(nameof(ghostCommands));
            _screenService = screenService ?? throw new ArgumentNullException(nameof(screenService));
            _buildingGhost = _ghostRepository.AddAndGet(cardId);
            _exitKey = gameKeysManager.GetKey(GameKeys.Exit);
            _exitKey.OnTap += OnExitTap;
            _buildingGhost.OnDispose += HandleOnHided;
        }

        protected override void DisposeInner()
        {
            _exitKey.OnTap -= OnExitTap;
            _buildingGhost.OnDispose -= HandleOnHided;
            _ghostRepository.Remove();
        }

        private void OnExitTap()
        {
            _screenService.Open<IMainScreenView>(x => x.Init());
        }

        private void HandleOnHided()
        {
            _screenService.Open<IMainScreenView>(x => x.Init());
        }

    }
}
