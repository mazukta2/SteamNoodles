using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Common.Services.Repositories
{
    public interface ISingletonRepository<T> : IRepository<T> where T : class, IEntity
    {
        T Add(T entity);
        void Remove();
        void Save(T entity);
        T Get();
        bool Has();
        ISingleQuery<T> AsQuery();
    }
}
