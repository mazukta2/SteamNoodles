using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Entities.Constructions
{
    public record Construction : Entity
    {
        public ConstructionScheme Scheme { get; }
        public FieldPosition Position { get; }
        public FieldRotation Rotation { get; }

        public Construction()
        {
            Scheme = new ConstructionScheme();
            Position = new FieldPosition(new Field(),0, 0);
            Rotation = FieldRotation.Default;
        }
        
        public Construction(Field field)
        {
            Scheme = new ConstructionScheme();
            Position = new FieldPosition(field,0, 0);
            Rotation = FieldRotation.Default;
        }
        
        public Construction(ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
        {
            Scheme = scheme;
            Position = position;
            Rotation = rotation;
        }

        public IReadOnlyCollection<FieldPosition> GetOccupiedSpace()
        {
            return Scheme.Placement.GetOccupiedSpace(Position, Rotation)
                .Select(x => x.AsFieldPosition(Position.Field)).AsReadOnly();
        }

        public IntRect GetSize()
        {
            return Scheme.Placement.GetRect(Rotation);
        }

        public GameVector3 GetWorldPosition()
        {
            return Position.GetWorldPosition(GetSize());
        }
    }
}
