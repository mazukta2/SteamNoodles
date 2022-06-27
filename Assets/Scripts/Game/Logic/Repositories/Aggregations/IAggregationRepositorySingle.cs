using System;
using Game.Assets.Scripts.Game.Logic.Common.Services;

namespace Game.Assets.Scripts.Game.Logic.Repositories.Aggregations
{
    public interface IAggregationRepositorySingle<T> : IService

    {
        T Get();
        event Action OnAdded;
        event Action OnRemoved;
    }
}