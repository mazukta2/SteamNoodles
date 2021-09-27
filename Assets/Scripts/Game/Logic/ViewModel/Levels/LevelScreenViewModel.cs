using Assets.Scripts.Logic;
using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Levels;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel
{
    public class LevelScreenViewModel
    {
        public HandViewModel Hand { get; }
        public CurrentOrderViewModel Order { get; private set; }

        private GameLevel _model;
        private ILevelView _view;
        private HistoryReader _reader;

        public LevelScreenViewModel(GameLevel model, ILevelView view, PlacementViewModel placement)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
            _view = view;
            Hand = new HandViewModel(model.Hand, _view.CreateHand(), placement);

            _reader = new HistoryReader(_model.Orders.History);
            _reader.Subscribe<CurrentOrderCreatedEvent>(UpdateOrder);
        }

        private void UpdateOrder(CurrentOrderCreatedEvent evt)
        {
            if (_model.Orders.CurrentOrder != null)
            {
                Order = new CurrentOrderViewModel(_model.Orders.CurrentOrder, _view.CreateCurrentOrder());
            }
            else
            {
                Order?.Destroy();
                Order = null;
            }
        }
    }
}
