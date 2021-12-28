using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    // this class is allow you to create instances of view inside list. and you can dispose them all if you want
    public class DisposableViewListKeeper<T> : Disposable where T : class, IView 
    {
        public T[] List => _list.Select(x => x.Value).ToArray();

        private Func<T> _spawn;
        private List<ValueKeeper> _list = new List<ValueKeeper>();

        public DisposableViewListKeeper(Func<T> spawn)
        {
            _spawn = spawn;
        }

        protected override void DisposeInner()
        {
            foreach (var item in _list)
                item.Dispose();
            _list.Clear();
        }

        public T Create()
        {
            var value = _spawn();
            var keeper = new ValueKeeper(value);
            keeper.OnDisposed += Keeper_OnDisposed;
            _list.Add(keeper);
            return value;
        }

        private void Keeper_OnDisposed(ValueKeeper keeper)
        {
            keeper.OnDisposed -= Keeper_OnDisposed;
            _list.Remove(keeper);
        }

        protected class ValueKeeper
        {
            public T Value;
            public event Action<ValueKeeper> OnDisposed = delegate { };

            public ValueKeeper(T value)
            {
                Value = value;
                Value.OnDispose += Value_OnDisposed;
            }

            public void Dispose()
            {
                Value.OnDispose -= Value_OnDisposed;
            }

            private void Value_OnDisposed()
            {
                OnDisposed(this);
                Dispose();
            }
        }
    }
}
