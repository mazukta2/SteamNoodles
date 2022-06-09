using Game.Assets.Scripts.Game.Logic.Common.Services.Events;
using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Models.Events.Constructions
{
    public record EntityAddedToRepositoryEvent<T> : IEvent where T : IEntity
    {
        public EntityAddedToRepositoryEvent(T entity) => Entity = entity;

        public T Entity { get; private set; }
    }
}
