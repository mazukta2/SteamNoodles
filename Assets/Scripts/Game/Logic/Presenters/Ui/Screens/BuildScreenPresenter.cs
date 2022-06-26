using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Services.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Services.Controls;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class BuildScreenPresenter : BasePresenter<IBuildScreenView>
    {
        private KeyCommand _exitKey;

        private IBuildScreenView _view;
        private readonly IDataProvider<ConstructionCardData> _entity;
        private readonly IDataProvider<GhostData> _ghost;
        private readonly IGhostCommands _ghostCommands;
        private readonly ScreenService _screenService;

        public BuildScreenPresenter(IBuildScreenView view, IDataProvider<ConstructionCardData> constructionCard) : this(
                view, constructionCard,
                IPresenterServices.Default?.Get<IDataProviderService<GhostData>>().Get(),
                IPresenterServices.Default?.Get<IGhostCommands>(),
                IPresenterServices.Default?.Get<ScreenService>(),
                IPresenterServices.Default?.Get<GameControlsService>())
        {
        }

        public BuildScreenPresenter(IBuildScreenView view,
            IDataProvider<ConstructionCardData> constructionCard,
            IDataProvider<GhostData> ghost,
            IGhostCommands ghostCommands,
            ScreenService screenService,
            GameControlsService gameKeysManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _entity = constructionCard ?? throw new ArgumentNullException(nameof(constructionCard));

            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _ghostCommands = ghostCommands ?? throw new ArgumentNullException(nameof(ghostCommands));
            _screenService = screenService ?? throw new ArgumentNullException(nameof(screenService));
            _ghostCommands.Show(_entity.Get().Id);
            _exitKey = gameKeysManager.GetKey(GameKeys.Exit);
            _exitKey.OnTap += OnExitTap;
            _ghost.OnRemoved += HandleOnHided;
        }

        protected override void DisposeInner()
        {
            _exitKey.OnTap -= OnExitTap;
            _ghost.OnRemoved -= HandleOnHided;
            _ghostCommands.Hide();
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
