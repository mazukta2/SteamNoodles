using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views
{
    public interface IView : IDisposable
    {
        public bool IsDisposed { get; }
    }
}
