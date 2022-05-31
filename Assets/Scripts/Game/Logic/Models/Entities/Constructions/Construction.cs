using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions
{
    public record Construction : Entity
    {
        public ConstructionScheme Scheme { get; }
        public FieldPosition Position { get; }
        public FieldRotation Rotation { get; }

        public Construction(ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
        {
            Scheme = scheme;
            Position = position;
            Rotation = rotation;
        }

        public IReadOnlyCollection<FieldPosition> GetOccupiedScace()
        {
            return Scheme.GetOccupiedSpace(Position, Rotation);
        }

        public IntRect GetSize()
        {
            return Scheme.Definition.GetRect(Rotation);
        }
    }
}
