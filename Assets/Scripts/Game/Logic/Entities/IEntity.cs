using System;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Entities
{
    public interface IEntity
    {
        Uid Id { get; }
        event Action<IEntity, IModelEvent> OnEvent;
    }
}
