﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Common.Core
{
    public abstract class Disposable : IDisposable
    {
        public event Action OnDispose = delegate { };
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

        protected abstract void DisposeInner();

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (!disposing)
                {
                    _undisposed.Add(this);
                }              
                DisposeInner();
                IsDisposed = true;
                OnDispose();
            }
        }


        private readonly static List<Disposable> _undisposed = new List<Disposable>();
        public static List<Disposable>  GetListOfUndisposed()
        {
            return _undisposed;
        }

        public static void ClearUndisopsed()
        {
            foreach (var item in _undisposed)
                item.Dispose();
            _undisposed.Clear();
        }
    }
}
