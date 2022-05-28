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

        //public Action OnExplode = delegate { };
        //public IntPoint CellPosition { get; private set; }
        //public FieldRotation Rotation { get; private set; }

        //public ConstructionSchemeEntity Definition { get; private set; }

        //private FieldPositionsCalculator _fieldPositions;

        //public Construction(ConstructionsSettingsDefinition settingsDefinition, ConstructionSchemeEntity definition, IntPoint position, FieldRotation rotation)
        //{
        //    Definition = definition ?? throw new ArgumentNullException(nameof(definition));
        //    CellPosition = position;
        //    Rotation = rotation;

        //    _fieldPositions = new FieldPositionsCalculator(settingsDefinition.CellSize);
        //}


        //public void Explode()
        //{
        //    OnExplode();
        //    Dispose();
        //}

    }
}
