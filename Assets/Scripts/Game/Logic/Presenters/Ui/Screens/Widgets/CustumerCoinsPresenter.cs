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

        private string _lastAnimation;
        private static bool _isEnabled;
        private static List<CustumerCoinsPresenter> _presenters = new List<CustumerCoinsPresenter>();

        public CustumerCoinsPresenter(Coins coins, ICustumerCoinsView view) : base(view)
        {
            _model = coins;
            _view = view;

            _model.OnChanged += UpdateView;
            UpdateView();
            UpdateAnimation();

            _presenters.Add(this);
        }

        protected override void DisposeInner()
        {
            _model.OnChanged -= UpdateView;
            _presenters.Remove(this);
        }

        public static void SetEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
            foreach (var item in _presenters)
                item.UpdateAnimation();
        }

        private void UpdateView()
        {
            _view.Text.Value = _model.Value.ToString();
        }

        private void UpdateAnimation()
        {
            if (IsDisposed)
                return;

            var animation = GetAnimation().ToString();
            if (string.IsNullOrEmpty(_lastAnimation) || animation != _lastAnimation)
            {
                _lastAnimation = GetAnimation().ToString();
                _view.Animator.Play(_lastAnimation);
            }
            else
            {
                _view.Animator.SwitchTo(_lastAnimation);
            }
        }

        private CoinsAnimations GetAnimation()
        {
            if (_isEnabled)
                return CoinsAnimations.Show;

            return CoinsAnimations.Hide;
        }

        public enum CoinsAnimations
        {
            None,
            Show,
            Hide
        }
    }
}
