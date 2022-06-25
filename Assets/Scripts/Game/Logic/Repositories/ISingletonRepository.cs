using System;
using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Common.Services.Repositories
{
    public interface ISingletonRepository<T> : IRepository<T> where T : class, IEntity
    {
        new event Action OnAdded;
        new event Action OnRemoved;
        new event Action<IModelEvent> OnEvent;
        
        void Remove();
        new T Get();
        bool Has();
        new ISingleQuery<T> AsQuery();
    }
}
