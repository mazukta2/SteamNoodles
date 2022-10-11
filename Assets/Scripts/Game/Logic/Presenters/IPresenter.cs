using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters
{
    public interface IPresenter
    {
        event Action OnDispose;
        bool IsDisposed { get; }
        void Dispose();
    }
}
