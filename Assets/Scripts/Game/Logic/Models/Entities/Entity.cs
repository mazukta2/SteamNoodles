using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Entities
{
    public abstract record Entity : IEntity
    {
        public Uid Id { get; }

        private RecordList<IModelEvent> _events = new RecordList<IModelEvent>();

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
            _events.Add(evt);
        }

        public IReadOnlyCollection<IModelEvent> GetEvents()
        {
            return _events.AsReadOnly();
        }

        public void Clear()
        {
            _events.Clear();
        }
    }
}
