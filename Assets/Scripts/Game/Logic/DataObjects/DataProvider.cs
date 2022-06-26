using System;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.DataObjects
{
    public class DataProvider<T> : IDataProvider<T> where T : class, IData
    {
        public event Action OnAdded = delegate {  };
        public event Action OnRemoved = delegate {  };
        public event Action<IModelEvent> OnEvent = delegate {  };

        private T _data;
        
        public DataProvider()
        {
        }
        
        public DataProvider(T data)
        {
            _data = data;
        }

        public void Add(T data)
        {
            _data = data;
            OnAdded();
        }
        
        public void Remove()
        {
            _data = null;
            OnRemoved();
        }

        public void FireEvent(IModelEvent e)
        {
            OnEvent(e);
        }
        
        public T Get()
        {
            return _data;
        }

        public bool Has()
        {
            return _data != null;
        }
    }
}