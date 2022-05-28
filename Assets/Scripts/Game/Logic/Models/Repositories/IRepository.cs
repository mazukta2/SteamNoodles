using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        void Add(T entity);
        void Remove(T entity);
        void Save(T entity);
        IReadOnlyCollection<T> Get();
        void FireEvent(T entity, IModelEvent modelEvent);
    }
}
