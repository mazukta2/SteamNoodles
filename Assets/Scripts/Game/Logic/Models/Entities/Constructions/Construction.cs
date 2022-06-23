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
        public CellPosition Position { get; }
        public FieldRotation Rotation { get; }

        public Construction()
        {
            Scheme = new ConstructionScheme();
            Position = new CellPosition(0, 0);
            Rotation = FieldRotation.Default;
        }
        
        public Construction(ConstructionScheme scheme, CellPosition position, FieldRotation rotation)
        {
            Scheme = scheme;
            Position = position;
            Rotation = rotation;
        }

        public IReadOnlyCollection<CellPosition> GetOccupiedScace()
        {
            return Scheme.Placement.GetOccupiedSpace(Position, Rotation);
        }

        public IntRect GetSize()
        {
            return Scheme.Placement.GetRect(Rotation);
        }
    }
}
