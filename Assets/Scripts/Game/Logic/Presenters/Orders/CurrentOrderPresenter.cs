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
        public ICurrentOrderView View { get; private set; }

        private ServingOrderProcess _model;

        public CurrentOrderPresenter(ServingOrderProcess model, ICurrentOrderView view)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (view == null) throw new ArgumentNullException(nameof(view));

            _model = model;
            View = view;
        }

        protected override void DisposeInner()
        {
            View.Dispose();
            View = null;
        }
    }
}
