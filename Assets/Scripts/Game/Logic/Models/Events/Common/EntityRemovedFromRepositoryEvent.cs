using Game.Assets.Scripts.Game.Logic.Common.Services.Events;
using Game.Assets.Scripts.Game.Logic.Models.Entities;

namespace Game.Assets.Scripts.Game.Logic.Models.Events.Constructions
{
    public record EntityRemovedFromRepositoryEvent<T>() : IEvent where T : IEntity;
}
