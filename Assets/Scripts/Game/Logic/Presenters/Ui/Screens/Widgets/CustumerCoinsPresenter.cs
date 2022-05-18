using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class CustumerCoinsPresenter : BasePresenter<ICustumerCoinsView>
    {
        private Coins _model;
        private ICustumerCoinsView _view;

        public CustumerCoinsPresenter(Coins coins, ICustumerCoinsView view) : base(view)
        {
            _model = coins;
            _view = view;

            _model.OnChanged += UpdateView;
            UpdateView();
        }

        protected override void DisposeInner()
        {
            _model.OnChanged -= UpdateView;
        }

        private void UpdateView()
        {
            _view.Text.Value = _model.Value.ToString();
        }
    }
}
