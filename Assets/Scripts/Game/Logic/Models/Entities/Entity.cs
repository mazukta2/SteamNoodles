using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Models.Entities
{
    public abstract record Entity : IEntity
    {
        public Uid Id { get; }

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


    }
}
