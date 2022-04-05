using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters
{
    public abstract class BasePresenter<TView, TPresenter> : PrivateDisposable 
        where TView : IPresenterView<TPresenter>
        where TPresenter : BasePresenter<TView, TPresenter>
    {
        private IView _view;
        public BasePresenter(TView view)
        {
            view.Presenter = (TPresenter)this;

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
