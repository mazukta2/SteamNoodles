using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Building
{
    public class ConstructionsManager : Disposable
    {
        public IReadOnlyCollection<PlacementField> Placements => _placements;

        private List<PlacementField> _placements = new List<PlacementField>();

        public ConstructionsManager(ConstructionsSettingsDefinition settings, LevelDefinition levelDefinition)
        {
            foreach (var field in levelDefinition.PlacementFields)
            {
                var placement = new PlacementField(settings, field);
                _placements.Add(placement);
            }
        }
        protected override void DisposeInner()
        {
            foreach (var item in _placements)
                item.Dispose();
            _placements.Clear();
        }
    }
}
