using System;
using Tests.Assets.Scripts.Game.Logic.Models.Orders;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Levels
{
    public class CurrentOrderViewModel
    {
        private CurrentOrder _model;

        public CurrentOrderViewModel(CurrentOrder model, ICurrentOrderView view)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
            View = view;
        }

        public ICurrentOrderView View { get; private set; }

        public void Destroy()
        {
            View.Destroy();
            View = null;
        }
    }
}
