using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.DataObjects
{
    public class DataQuery<T> : Disposable, IDataQuery<T>
    {
        private readonly IDataQueryHandler<T> _handler;
        
        public event Action OnAdded = delegate {  };
        public event Action OnRemoved = delegate {  };
        public event Action<IModelEvent> OnEvent = delegate {  };

        public DataQuery(IDataQueryHandler<T> handler)
        {
            _handler = handler;
        }

        public T Get()
        {
            return _handler.Get();
        }

        public bool Has()
        {
            return _handler.Get() != null;
        }
    }
}