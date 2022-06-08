using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class BuildScreenPresenter : BasePresenter<IBuildScreenView>
    {
        private KeyCommand _exitKey;

        private IBuildScreenView _view;
        private readonly ConstructionCard _entity;
        private readonly BuildingModeService _buildingModeService;

        public BuildScreenPresenter(IBuildScreenView view, ConstructionCard constructionCard) : this(
                view, constructionCard,
                IPresenterServices.Default?.Get<BuildingModeService>(),
                IGameKeysManager.Default)
        {
        }

        public BuildScreenPresenter(IBuildScreenView view,
            ConstructionCard constructionCard,
            BuildingModeService buildingModeService,
            IGameKeysManager gameKeysManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _entity = constructionCard ?? throw new ArgumentNullException(nameof(constructionCard));

            _buildingModeService = buildingModeService ?? throw new ArgumentNullException(nameof(buildingModeService));
            _buildingModeService.Show(_entity);
            _exitKey = gameKeysManager.GetKey(GameKeys.Exit);
            _exitKey.OnTap += OnExitTap;
        }

        protected override void DisposeInner()
        {
            _exitKey.OnTap -= OnExitTap;
            _buildingModeService.Hide();
        }

        private void OnExitTap()
        {
            ScreenManagerPresenter.Default.Open<IMainScreenView>(x => new MainScreenPresenter(x));
        }

    }
}
