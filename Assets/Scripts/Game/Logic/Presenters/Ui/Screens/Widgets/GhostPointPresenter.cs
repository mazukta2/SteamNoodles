﻿using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class GhostPointPresenter : BasePresenter<IGhostPointsView>
    {
        private IGhostPointsView _view;
        private readonly GhostService _ghostService;
        private readonly ConstructionsService _constructionsService;
        private readonly Field _field;

        //private Dictionary<Construction, IAdjacencyTextView> _bonuses = new Dictionary<Construction, IAdjacencyTextView>();

        public GhostPointPresenter(IGhostPointsView view) : this(
                view,
                IPresenterServices.Default?.Get<GhostService>(),
                IPresenterServices.Default?.Get<ConstructionsService>(),
                IPresenterServices.Default.Get<ISingletonRepository<Field>>().Get())
        {
        }

        public GhostPointPresenter(IGhostPointsView view,
            GhostService ghostService,
            ConstructionsService constructionsService,
            Field field) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghostService = ghostService ?? throw new ArgumentNullException(nameof(ghostService));
            _constructionsService = constructionsService ?? throw new ArgumentNullException(nameof(constructionsService));
            _field = field ?? throw new ArgumentNullException(nameof(Field));

            _ghostService.OnChanged += UpdatePoints;
            _ghostService.OnShowed += UpdatePoints;
            _ghostService.OnHided += UpdatePoints;
            UpdatePoints();
        }


        protected override void DisposeInner()
        {
            _ghostService.OnChanged -= UpdatePoints;
            _ghostService.OnShowed -= UpdatePoints;
            _ghostService.OnHided -= UpdatePoints;
        }

        public void UpdatePoints()
        {
            if (_ghostService.IsEnabled())
            {
                var ghost = _ghostService.GetGhost();
                var points = _constructionsService.GetPoints(ghost.Card, ghost.Position, ghost.Rotation);

                var worldPosition = _field.GetWorldPosition(ghost.Position,
                    ghost.Card.Scheme.Placement.GetRect(ghost.Rotation));

                _view.Points.Value = points.AsString();
                _view.Points.Position = worldPosition;
            }
            else
            {
                _view.Points.Value = "";
            }

            //UpdateBonuses(bonuses);
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
            //    text.Position = _Field.GetWorldPosition(item.Key);
            //}

            //_buildingModeService.SetHighlight(_bonuses.Keys.ToArray().AsReadOnly());
        }

        private void OnExitTap()
        {
            //ScreenManagerPresenter.Default.Open<IMainScreenView>(x => new MainScreenPresenter(x));
        }

    }
}
