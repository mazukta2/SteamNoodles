using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using System;
using System.Collections.Generic;
using Tests.Assets.Scripts.Game.Logic.Presenters.Levels;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Orders
{
    public class CurrentOrderPresenter : Disposable
    {
        private ICurrentOrderView _view;
        private ServingCustomerProcess _model;

        public CurrentOrderPresenter(ServingCustomerProcess model, ICurrentOrderView view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        protected override void DisposeInner()
        {
            _view.Dispose();
            _view = null;
        }
    }
}
