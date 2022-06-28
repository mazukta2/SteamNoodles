using System;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Entities
{
    public abstract record Entity : IEntity
    {
        public Uid Id { get; }
        public event Action<IEntity, IModelEvent> OnEvent = delegate { };

        public Entity(Uid id)
        {
            Id = id;
        }

        public Entity()
        {
            Id = new Uid();
        }

        public bool Compare(Entity other)
        {
            return other != null && other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.Value.GetHashCode();
        }

        public IEntity Copy()
        {
            return this with { };
        }

        protected void FireEvent(IModelEvent evt)
        {
            OnEvent(this, evt);
        }
    }
}
