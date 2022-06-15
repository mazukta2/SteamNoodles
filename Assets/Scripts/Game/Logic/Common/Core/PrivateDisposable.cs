using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Common.Core
{
    public abstract class PrivateDisposable
    {
        public event Action OnDispose = delegate { };
        public bool IsDisposed { get; private set; }

        public PrivateDisposable()
        {
            _stackTrace = System.Environment.StackTrace;
        }
        
        protected void Dispose() // Implement IDisposable
        {
            Dispose(true);
#if DEBUG
            GC.SuppressFinalize(this);
#endif
        }

#if DEBUG
        ~PrivateDisposable() // the finalizer
        {
            Dispose(false);
        }
#endif

        protected virtual void DisposeInner() { }

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


        private readonly static List<PrivateDisposable> _undisposed = new List<PrivateDisposable>();
        private readonly string _stackTrace;

        public static Dictionary<PrivateDisposable, string> GetListOfUndisposed()
        {
            return _undisposed.ToDictionary(x => x, y => y._stackTrace);
        }

        public static void ClearUndisopsed()
        {
            foreach (var item in _undisposed)
                item.Dispose();
            _undisposed.Clear();
        }
    }
}
