using Assets.Scripts.Logic;
using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.ViewModel;
using Game.Assets.Scripts.Game.Logic.ViewModel.Constructions;
using Game.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.ViewModel.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel
{
    public class LevelScreenViewModel : IViewModel
    {
        public HandViewModel Hand { get; }
        public CurrentOrderViewModel Order { get; private set; }
        public bool IsDestoyed { get; private set; }

        private GameLevel _model;
        private ILevelView _view;

        public LevelScreenViewModel(GameLevel model, ILevelView view, PlacementViewModel placement)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
            _view = view;
            Hand = new HandViewModel(model.Hand, _view.CreateHand(), placement);

            model.Orders.OnCurrentOrderChanged += UpdateOrder;
        }

        public void Destroy()
        {
            _model.Orders.OnCurrentOrderChanged -= UpdateOrder;
            Hand.Destroy();
            Order?.Destroy();
            IsDestoyed = true;
        }

        private void UpdateOrder()
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
