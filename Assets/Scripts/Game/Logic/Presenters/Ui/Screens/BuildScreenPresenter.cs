using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
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
        private readonly ConstructionCard _entity;
        private readonly ISingleQuery<ConstructionGhost> _ghost;
        private readonly IGhostCommands _ghostCommands;
        private readonly ScreenService _screenService;

        public BuildScreenPresenter(IBuildScreenView view, ConstructionCard constructionCard) : this(
                view, constructionCard,
                IPresenterServices.Default?.Get<ISingletonRepository<ConstructionGhost>>().AsQuery(),
                IPresenterServices.Default?.Get<IGhostCommands>(),
                IPresenterServices.Default?.Get<ScreenService>(),
                IPresenterServices.Default?.Get<GameControlsService>())
        {
        }

        public BuildScreenPresenter(IBuildScreenView view,
            ConstructionCard constructionCard,
            ISingleQuery<ConstructionGhost> ghost,
            IGhostCommands ghostCommands,
            ScreenService screenService,
            GameControlsService gameKeysManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _entity = constructionCard ?? throw new ArgumentNullException(nameof(constructionCard));

            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _ghostCommands = ghostCommands ?? throw new ArgumentNullException(nameof(ghostCommands));
            _screenService = screenService ?? throw new ArgumentNullException(nameof(screenService));
            _ghostCommands.Show(_entity);
            _exitKey = gameKeysManager.GetKey(GameKeys.Exit);
            _exitKey.OnTap += OnExitTap;
            _ghost.OnRemoved += HandleOnHided;
        }

        protected override void DisposeInner()
        {
            _exitKey.OnTap -= OnExitTap;
            _ghost.OnRemoved -= HandleOnHided;
            _ghostCommands.Hide();
            
            _ghost.Dispose();
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
