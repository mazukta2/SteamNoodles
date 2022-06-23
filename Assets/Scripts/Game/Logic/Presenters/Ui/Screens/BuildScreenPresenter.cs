using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class BuildScreenPresenter : BasePresenter<IBuildScreenView>
    {
        private KeyCommand _exitKey;

        private IBuildScreenView _view;
        private readonly ConstructionCard _entity;
        private readonly GhostService _ghostService;
        private readonly ScreenService _screenService;

        public BuildScreenPresenter(IBuildScreenView view, ConstructionCard constructionCard) : this(
                view, constructionCard,
                IPresenterServices.Default?.Get<GhostService>(),
                IPresenterServices.Default?.Get<ScreenService>(),
                IPresenterServices.Default?.Get<GameControlsService>())
        {
        }

        public BuildScreenPresenter(IBuildScreenView view,
            ConstructionCard constructionCard,
            GhostService ghostService,
            ScreenService screenService,
            GameControlsService gameKeysManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _entity = constructionCard ?? throw new ArgumentNullException(nameof(constructionCard));

            _ghostService = ghostService ?? throw new ArgumentNullException(nameof(ghostService));
            _screenService = screenService ?? throw new ArgumentNullException(nameof(screenService));
            _ghostService.Show(_entity);
            _exitKey = gameKeysManager.GetKey(GameKeys.Exit);
            _exitKey.OnTap += OnExitTap;
            _ghostService.OnHided += HandleOnHided;
        }

        protected override void DisposeInner()
        {
            _exitKey.OnTap -= OnExitTap;
            _ghostService.OnHided -= HandleOnHided;
            _ghostService.Hide();
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
