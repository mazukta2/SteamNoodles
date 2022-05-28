using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Repositories
{
    public interface ISingletonRepository<T> where T : class, IEntity
    {
        void Add(T entity);
        void Remove(T entity);
        void Save(T entity);
        T Get();
        bool Has();
    }
}
