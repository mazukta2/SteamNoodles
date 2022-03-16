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
        public IntPoint Size => _field.Size;
        public IntRect Rect => Size.AsCenteredRect();
        public FloatRect RealRect => Rect * _settings.CellSize;

        private ConstructionsSettingsDefinition _settings;
        private PlacementFieldDefinition _field;

        public PlacementField(ConstructionsSettingsDefinition settings, PlacementFieldDefinition definition)
        {
            _settings = settings;
            _field = definition;
        }

        protected override void DisposeInner()
        {
        }
    }
}
