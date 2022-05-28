﻿using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Builders;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Elements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class BuildScreenPresenter : BasePresenter<IBuildScreenView>
    {
        private KeyCommand _exitKey;

        public ConstructionCard CurrentCard { get; }

        private IBuildScreenView _view;
        private ConstructionsSettingsDefinition _constrcutionsSettings;
        private readonly BuildingTooltipPresenter _tooltip;
        private readonly ScreenManagerPresenter _screenManager;
        private readonly IFieldPresenterService _fieldService;
        private Dictionary<Construction, IAdjacencyTextView> _bonuses = new Dictionary<Construction, IAdjacencyTextView>();

        public BuildScreenPresenter(IBuildScreenView view,
            ConstructionCard constructionCard, ConstructionsSettingsDefinition constrcutionsSettings, 
            HandPresenter handPresenter, BuildingTooltipPresenter tooltip,
            IGameKeysManager gameKeysManager, ScreenManagerPresenter screenManager, IFieldPresenterService fieldService) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _constrcutionsSettings = constrcutionsSettings ?? throw new ArgumentNullException(nameof(constrcutionsSettings));
            _tooltip = tooltip;
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
            handPresenter.Mode = HandPresenter.Modes.Build;
            _tooltip.Show(constructionCard);
            _exitKey = gameKeysManager.GetKey(GameKeys.Exit);
            _exitKey.OnTap += OnExitTap;

            CurrentCard = constructionCard;
        }

        protected override void DisposeInner()
        {
            _exitKey.OnTap -= OnExitTap;
            _tooltip.Hide();
        }

        public void UpdatePoints(GameVector3 position, int points, IReadOnlyDictionary<Construction, BuildingPoints> bonuses)
        {
            _view.Points.Value = $"{points.GetSignedNumber()}";
            _view.Points.Position = position;

            UpdateBonuses(bonuses);
        }

        private void UpdateBonuses(IReadOnlyDictionary<Construction, BuildingPoints> newBonuses)
        {
            foreach (var item in _bonuses.ToList())
            {
                if (!newBonuses.ContainsKey(item.Key))
                {
                    _bonuses[item.Key].Dispose();
                    _bonuses.Remove(item.Key);
                }    
            }

            foreach (var item in newBonuses)
            {
                if (!_bonuses.ContainsKey(item.Key))
                {
                    var view = _view.AdjacencyContainer.Spawn<IAdjacencyTextView>(_view.AdjacencyPrefab);
                    _bonuses[item.Key] = view;
                }
            }

            foreach (var item in newBonuses)
            {
                var text =_bonuses[item.Key].Text;
                text.Value = $"{item.Value}";
                text.Position = _fieldService.GetWorldPosition(item.Key);
            }

            _tooltip.SetHighlight(_bonuses.Keys.ToArray());
        }

        private void OnExitTap()
        {
            _screenManager.GetCollection<CommonScreens>().Open<IMainScreenView>();
        }

    }
}
