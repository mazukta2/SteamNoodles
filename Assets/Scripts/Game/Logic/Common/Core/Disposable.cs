using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Common.Core
{
    public abstract class Disposable : IDisposable
    {
        public bool IsDisposed { get; private set; }
        public void Dispose() // Implement IDisposable
        {
            Dispose(true);
#if DEBUG
            GC.SuppressFinalize(this);
#endif
        }

#if DEBUG
        ~Disposable() // the finalizer
        {
            Dispose(false);
        }
#endif

        protected abstract void OnDispose();

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (!disposing)
                {
                    throw new Exception("Undisposed action " + this);
                    //_undisposed.Add(this);
                }              
                OnDispose();
                IsDisposed = true;
            }
        }
    }
}
