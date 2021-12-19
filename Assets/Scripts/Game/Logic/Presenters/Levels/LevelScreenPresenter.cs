using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Orders;
using Game.Assets.Scripts.Game.Logic.Presenters.Units;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Levels
{
    public class LevelScreenPresenter : Disposable
    {
        public HandPresenter Hand { get; }
        public CurrentOrderPresenter Order { get; private set; }
        public ClashesPresenter Clashes { get; private set; }


        private readonly GameLevel _model;
        private readonly IScreenView _view;

        private LevelResourcesPresenter _resources;

        public LevelScreenPresenter(GameLevel model, IScreenView view, PlacementPresenter placement)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            Hand = new HandPresenter(model.Hand, _view.Hand.Create(), placement);
            Clashes = new ClashesPresenter(model.Clashes, view.Clashes.Create());
            _resources = new LevelResourcesPresenter(model, view.Resources.Create());

            //model.Customers.OnCurrentCustomerChanged += UpdateOrder;
        }

        protected override void DisposeInner()
        {
            //_model.Customers.OnCurrentCustomerChanged -= UpdateOrder;
            Hand.Dispose();
            Order?.Dispose();
            Clashes.Dispose();
            _resources.Dispose();
        }

        private void UpdateOrder()
        {
            //if (_model.Customers.ServingCustomer != null)
            //{
            //    Order = new CurrentOrderPresenter(_model.Customers.ServingCustomer, _view.Customers.Create());
            //}
            //else
            //{
            //    Order?.Dispose();
            //    Order = null;
            //}
        }

    }
}
