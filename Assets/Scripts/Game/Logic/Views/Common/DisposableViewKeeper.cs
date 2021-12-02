using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public class DisposableViewKeeper<T> : Disposable where T : class, IView 
    {
        public T Value { get; private set; }
        private Func<T> _spawn;

        public DisposableViewKeeper(Func<T> spawn)
        {
            _spawn = spawn;
        }

        protected override void DisposeInner()
        {
            if (Value != null)
                Value.OnDispose -= Value_OnDisposed;
        }

        public T Create()
        {
            if (Value != null)
                throw new Exception("You can't spawn in existing keeper");

            Value = _spawn();
            Value.OnDispose += Value_OnDisposed;
            return Value;
        }

        private void Value_OnDisposed()
        {
            Value = null;
        }

    }
}
