using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.DataObjects
{
    public interface IDataCollectionProvider<T> where T : class, IData
    {
        event Action<IDataProvider<T>> OnAdded;
        event Action<IDataProvider<T>> OnRemoved;
        event Action<IDataProvider<T>, IModelEvent> OnEvent;
        IReadOnlyCollection<IDataProvider<T>> Get();
    }
}