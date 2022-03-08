using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Unity.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters
{
    public abstract class BasePresenter : PrivateDisposable
    {
        private View _view;
        public BasePresenter(View view)
        {
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
