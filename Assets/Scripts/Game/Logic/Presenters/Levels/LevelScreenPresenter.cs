using Assets.Scripts.Logic;
using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Orders;
using Game.Assets.Scripts.Game.Logic.Presenters.Units;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Levels
{
    public class LevelScreenPresenter : Disposable
    {
        public HandPresenter Hand { get; }
        public CurrentOrderPresenter Order { get; private set; }
        public ClashesPresenter Clashes { get; private set; }

        private GameLevel _model;
        private IScreenView _view;

        public LevelScreenPresenter(GameLevel model, IScreenView view, PlacementPresenter placement)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (view == null) throw new ArgumentNullException(nameof(view));

            _model = model;
            _view = view;
            Hand = new HandPresenter(model.Hand, _view.CreateHand(), placement);
            Clashes = new ClashesPresenter(model.Clashes, view.CreateClashes());

            model.Orders.OnCurrentOrderChanged += UpdateOrder;
        }

        protected override void DisposeInner()
        {
            _model.Orders.OnCurrentOrderChanged -= UpdateOrder;
            Hand.Dispose();
            Order?.Dispose();
            Clashes.Dispose();
        }

        private void UpdateOrder()
        {
            if (_model.Orders.CurrentOrder != null)
            {
                Order = new CurrentOrderPresenter(_model.Orders.CurrentOrder, _view.CreateCurrentOrder());
            }
            else
            {
                Order?.Dispose();
                Order = null;
            }
        }

    }
}
