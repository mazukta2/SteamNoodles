using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters
{
    public abstract class BasePresenter<TView> : PrivateDisposable, IPresenter
        where TView : IView, IViewWithPresenter
    {
        private IView _view;
        public BasePresenter(TView view)
        {
            view.Presenter = this;

            _view = view ?? throw new ArgumentNullException(nameof(view));
            if (_view.IsDisposed) throw new ArgumentException(nameof(view) + " is disposed");

            _view.OnDispose += _model_OnDispose;
        }

        private void _model_OnDispose()
        {
            _view.OnDispose -= _model_OnDispose;
            _view = null;
            Dispose();
        }
    }
}
