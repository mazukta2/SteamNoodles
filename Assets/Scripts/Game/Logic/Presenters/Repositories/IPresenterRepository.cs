using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Repositories
{
    public interface IPresenterRepository<T> where T : class
    {
        event Action<EntityLink<T>, T> OnAdded;
        event Action<EntityLink<T>, T> OnRemoved;
        event Action<EntityLink<T>, T> OnChanged;
        event Action<EntityLink<T>, T, IModelEvent> OnEvent;
        IReadOnlyCollection<EntityLink<T>> Get();
        IReadOnlyCollection<T> GetAll();
        T Get(Uid uid);
    }
}
