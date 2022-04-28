using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Building
{
    public class ConstructionsManager : Disposable
    {
        public PlacementField Placement => _placements;

        private PlacementField _placements;

        public ConstructionsManager(ConstructionsSettingsDefinition settings, LevelDefinition levelDefinition, Resources resources, TurnManager turnManager)
        {
            _placements = new PlacementField(settings, levelDefinition.PlacementField, resources, turnManager);
        }

        protected override void DisposeInner()
        {
            _placements.Dispose();
        }
    }
}
