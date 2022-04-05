using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views
{
    public interface IView
    {
        event Action OnDispose;
        bool IsDisposed { get; }
        void Dispose();
    }
}
