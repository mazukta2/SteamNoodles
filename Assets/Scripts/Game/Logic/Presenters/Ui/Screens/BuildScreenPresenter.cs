using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Builders;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Elements;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class BuildScreenPresenter : BasePresenter<IBuildScreenView>
    {
        private KeyCommand _exitKey;

        public ConstructionCard CurrentCard => _entity.Get();

        private IBuildScreenView _view;
        private readonly EntityLink<ConstructionCard> _entity;
        private readonly BuildingModeService _buildingModeService;

        //private Dictionary<Construction, IAdjacencyTextView> _bonuses = new Dictionary<Construction, IAdjacencyTextView>();

        public BuildScreenPresenter(IBuildScreenView view, EntityLink<ConstructionCard> constructionCard) : this(
                view, constructionCard,
                IPresenterServices.Default?.Get<BuildingModeService>(), 
                IGameKeysManager.Default)
        {
        }

        public BuildScreenPresenter(IBuildScreenView view,
            EntityLink<ConstructionCard> constructionCard,
            BuildingModeService buildingModeService,
            IGameKeysManager gameKeysManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _buildingModeService = buildingModeService ?? throw new ArgumentNullException(nameof(buildingModeService));

            _buildingModeService.Show(constructionCard);
            _exitKey = gameKeysManager.GetKey(GameKeys.Exit);
            _exitKey.OnTap += OnExitTap;

            _entity = constructionCard;
        }

        protected override void DisposeInner()
        {
            _exitKey.OnTap -= OnExitTap;
            _buildingModeService.Hide();
        }

        public void UpdatePoints(GameVector3 position, int points, IReadOnlyDictionary<Construction, BuildingPoints> bonuses)
        {
            _view.Points.Value = $"{points.GetSignedNumber()}";
            _view.Points.Position = position;

            UpdateBonuses(bonuses);
        }

        private void UpdateBonuses(IReadOnlyDictionary<Construction, BuildingPoints> newBonuses)
        {
            //foreach (var item in _bonuses.ToList())
            //{
            //    if (!newBonuses.ContainsKey(item.Key))
            //    {
            //        _bonuses[item.Key].Dispose();
            //        _bonuses.Remove(item.Key);
            //    }    
            //}

            //foreach (var item in newBonuses)
            //{
            //    if (!_bonuses.ContainsKey(item.Key))
            //    {
            //        var view = _view.AdjacencyContainer.Spawn<IAdjacencyTextView>(_view.AdjacencyPrefab);
            //        _bonuses[item.Key] = view;
            //    }
            //}

            //foreach (var item in newBonuses)
            //{
            //    var text =_bonuses[item.Key].Text;
            //    text.Value = $"{item.Value}";
            //    text.Position = _fieldService.GetWorldPosition(item.Key);
            //}

            //_buildingModeService.SetHighlight(_bonuses.Keys.ToArray().AsReadOnly());
        }

        private void OnExitTap()
        {
            ScreenManagerPresenter.Default.Open<IMainScreenView>(x => new MainScreenPresenter(x));
        }

    }
}
