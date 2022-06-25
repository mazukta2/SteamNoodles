using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.DataObjects
{
    public class StaticDataQuery<T> : Disposable, IDataQuery<T> where T : class, IData
    {
        private T _data;
        
        public event Action OnAdded = delegate {  };
        public event Action OnRemoved = delegate {  };
        public event Action<IModelEvent> OnEvent = delegate {  };

        public StaticDataQuery()
        {
        }
        
        public StaticDataQuery(T data)
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