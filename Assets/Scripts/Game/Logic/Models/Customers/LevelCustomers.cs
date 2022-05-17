using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers
{
    public class LevelCustomers : Disposable, ICustomers
    {
        private readonly PlacementField _placementField;
        private readonly LevelDefinition _levelDefinition;
        private readonly UnitsSettingsDefinition _unitsSettings;
        private readonly Resources _resources;

        public LevelCustomers(PlacementField placementField, LevelDefinition levelDefinition, UnitsSettingsDefinition unitsSettings, Levels.Resources resources)
        {
            _placementField = placementField ?? throw new ArgumentNullException(nameof(placementField));
            _levelDefinition = levelDefinition ?? throw new ArgumentNullException(nameof(levelDefinition));
            _unitsSettings = unitsSettings ?? throw new ArgumentNullException(nameof(unitsSettings));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
        }

        public float SpawnAnimationDelay => _unitsSettings.SpawnAnimationDelay;

        public GameVector3 GetQueueFirstPosition()
        {
            var construction = _placementField.Constructions.First();
            var queueStartingPosition = construction.GetWorldPosition().X;
            return new GameVector3(queueStartingPosition, 0, _levelDefinition.QueuePosition.Z);
        }

        public GameVector3 GetQueueFirstPositionOffset()
        {
            return _levelDefinition.QueueFirstPositionOffset;
        }

        public int GetQueueSize()
        {
            return _resources.Points.CurrentLevel;
        }
    }
}
