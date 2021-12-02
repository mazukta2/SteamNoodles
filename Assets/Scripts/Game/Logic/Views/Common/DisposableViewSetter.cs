using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public class DisposableViewSetter<T> : Disposable where T : class, IView 
    {
        public T Value { get; private set; }

        public DisposableViewSetter()
        {
        }

        protected override void DisposeInner()
        {
            if (Value != null)
                Value.OnDispose -= Value_OnDisposed;
        }

        public void Set(T value)
        {
            if (Value != null)
                throw new Exception("You can't spawn in existing keeper");

            Value = value;
            Value.OnDispose += Value_OnDisposed;
        }

        private void Value_OnDisposed()
        {
            Value = null;
        }

    }
}
