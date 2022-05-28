using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers
{
    public class LevelCustomers : Disposable, ICustomers
    {
        private readonly IRepository<Construction> _constructions;
        private readonly FieldService _fieldPositionService;
        private readonly LevelDefinition _levelDefinition;
        private readonly UnitsSettingsDefinition _unitsSettings;
        private readonly Resources _resources;
        private int _queueSize = 0;

        public LevelCustomers(IRepository<Construction> constructions, FieldService fieldPositionService, LevelDefinition levelDefinition, UnitsSettingsDefinition unitsSettings, Levels.Resources resources)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _fieldPositionService = fieldPositionService ?? throw new ArgumentNullException(nameof(fieldPositionService));
            _levelDefinition = levelDefinition ?? throw new ArgumentNullException(nameof(levelDefinition));
            _unitsSettings = unitsSettings ?? throw new ArgumentNullException(nameof(unitsSettings));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            _resources.Points.OnTargetLevelChanged += Points_OnTargetLevelChanged;
        }

        protected override void DisposeInner()
        {
            _resources.Points.OnTargetLevelChanged -= Points_OnTargetLevelChanged;
        }

        public float SpawnAnimationDelay => _unitsSettings.SpawnAnimationDelay;

        public GameVector3 GetQueueFirstPosition()
        {
            var construction = _constructions.Get().First();
            var queueStartingPosition = _fieldPositionService.GetWorldPosition(construction).X;
            return new GameVector3(queueStartingPosition, 0, _levelDefinition.QueuePosition.Z);
        }

        public GameVector3 GetQueueFirstPositionOffset()
        {
            return _levelDefinition.QueueFirstPositionOffset;
        }

        public int GetQueueSize()
        {
            return _queueSize;
        }

        public void ClearQueue()
        {
            _queueSize = 0;
        }

        private void Points_OnTargetLevelChanged(int changes)
        {
            _queueSize += changes;
            if (_queueSize < 0)
                _queueSize = 0;
        }

        public void Serve(Unit unit)
        {
            _resources.Coins.Change(_unitsSettings.BaseCoins);
        }
    }
}
