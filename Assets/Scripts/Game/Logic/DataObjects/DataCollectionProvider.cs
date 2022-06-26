using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.DataObjects
{
    public class DataCollectionProvider<T> : IDataCollectionProvider<T> where T : class, IData
    {
        public event Action<IDataProvider<T>> OnAdded = delegate { };
        public event Action<IDataProvider<T>> OnRemoved = delegate { };
        public event Action<IDataProvider<T>, IModelEvent> OnEvent = delegate { };
        public IReadOnlyCollection<IDataProvider<T>> Get()
        {
            throw new NotImplementedException();
        }

        public DataCollectionProvider()
        {
        }
        
        public DataCollectionProvider(T data)
        {
        }

    }
}