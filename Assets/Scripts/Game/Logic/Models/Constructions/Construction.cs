using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Constructions
{
    public class Construction : Disposable
    {
        public IntPoint CellPosition { get; private set; }
        public FieldRotation Rotation { get; private set; }

        public ConstructionDefinition Definition { get; private set; }

        private FieldPositionsCalculator _fieldPositions;

        public Construction(ConstructionsSettingsDefinition settingsDefinition, ConstructionDefinition definition, IntPoint position, FieldRotation rotation)
        {
            Definition = definition ?? throw new ArgumentNullException(nameof(definition));
            CellPosition = position;
            Rotation = rotation;

            _fieldPositions = new FieldPositionsCalculator(settingsDefinition.CellSize);
        }

        public IntRect GetSize()
        {
            return Definition.GetRect(Rotation);
        }

        public IReadOnlyCollection<IntPoint> GetOccupiedScace()
        {
            return Definition.GetOccupiedSpace(CellPosition, Rotation);
        }

        public void Destroy()
        {
            Dispose();
        }

        public FloatPoint3D GetWorldPosition()
        {
            return _fieldPositions.GetMapPositionByGridPosition(CellPosition, GetSize());
        }
    }
}
