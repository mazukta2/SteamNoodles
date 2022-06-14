using Game.Assets.Scripts.Game.Logic.Models.Entities;

namespace Game.Assets.Scripts.Game.Logic.Common.Services.Repositories
{
    public interface ISingletonRepository<T> : IBaseRepository where T : class, IEntity
    {
        void Add(T entity);
        void Remove(T entity);
        void Save(T entity);
        T Get();
        bool Has();
    }
}
