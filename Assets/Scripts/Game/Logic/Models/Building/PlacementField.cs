using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Building
{
    public class PlacementField : Disposable
    {
        public ConstructionsSettingsDefinition ConstructionsSettings { get; private set; }
        public IntPoint Size => _field.Size;
        public IntRect Rect { get; private set; }
        private PlacementFieldDefinition _field;

        public PlacementField(ConstructionsSettingsDefinition settings, PlacementFieldDefinition definition)
        {
            ConstructionsSettings = settings ?? throw new ArgumentNullException(nameof(settings));
            _field = definition ?? throw new ArgumentNullException(nameof(definition));

            Rect = new IntRect(-Size.X / 2, -Size.Y / 2, Size.X, Size.Y);
        }

        protected override void DisposeInner()
        {
        }
    }
}
