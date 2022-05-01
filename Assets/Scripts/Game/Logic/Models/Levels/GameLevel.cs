using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class GameLevel : Disposable
    {
        public FlowManager TurnManager { get; }
        public PlayerHand Hand { get; private set; }
        public PlacementField Constructions { get; private set; }
        public LevelUnits Units { get; }

        private LevelCrowd _crowd;
        private LevelQueue _queue;

        public Resources Resources { get; }
        public LevelDefinition Definition { get; private set; }

        private SessionRandom _random;

        public GameLevel(LevelDefinition settings, SessionRandom random, IGameTime time, IDefinitions definitions)
        {
            Definition = settings ?? throw new ArgumentNullException(nameof(settings));
            _random = random ?? throw new ArgumentNullException(nameof(random));
            if (time == null) throw new ArgumentNullException(nameof(time));

            Hand = new PlayerHand(settings, settings.StartingHand);
            Resources = new Resources(definitions.Get<ConstructionsSettingsDefinition>());

            var unitSettings = definitions.Get<UnitsSettingsDefinition>();
            Units = new LevelUnits(time);
            _crowd = new LevelCrowd(unitSettings, Units, time, settings, random);

            Constructions = new PlacementField(definitions.Get<ConstructionsSettingsDefinition>(), Definition.PlacementField, Resources);
            TurnManager = new FlowManager(Definition, random, Constructions, Hand);
            _queue = new LevelQueue(unitSettings, Units, settings, random, Resources.Points, Constructions, TurnManager);

            Resources.Points.OnMaxLevelUp += OnLevelUp;
        }

        protected override void DisposeInner()
        {
            Resources.Points.OnMaxLevelUp -= OnLevelUp;

            TurnManager.Dispose();
            Hand.Dispose();
            Constructions.Dispose();
            Units.Dispose();
            Resources.Dispose();
            _crowd.Dispose();
            _queue.Dispose();
        }

        private void OnLevelUp()
        {
            TurnManager.GiveCards();
        }

    }
}
