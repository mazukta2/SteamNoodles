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
        public TurnManager TurnManager { get; }
        public PlayerHand Hand { get; private set; }
        public ConstructionsManager Constructions { get; private set; }
        public LevelUnits Units { get; }

        private LevelCrowd _crowd;
        private LevelQueue _queue;

        public Resources Resources { get; }

        private LevelDefinition _definition;
        private SessionRandom _random;
        private Deck<ConstructionDefinition> _rewardDeck;

        public GameLevel(LevelDefinition settings, SessionRandom random, IGameTime time, IDefinitions definitions)
        {
            _definition = settings ?? throw new ArgumentNullException(nameof(settings));
            _random = random ?? throw new ArgumentNullException(nameof(random));
            if (time == null) throw new ArgumentNullException(nameof(time));

            TurnManager = new TurnManager();
            Hand = new PlayerHand(settings, settings.StartingHand);
            Resources = new Resources(definitions.Get<ConstructionsSettingsDefinition>());

            var unitSettings = definitions.Get<UnitsSettingsDefinition>();
            Units = new LevelUnits(time);
            _crowd = new LevelCrowd(unitSettings, Units, time, settings, random);

            Constructions = new ConstructionsManager(definitions.Get<ConstructionsSettingsDefinition>(), _definition, Resources, TurnManager);
            _queue = new LevelQueue(unitSettings, Units, settings, random, Resources.Points, Constructions, TurnManager);

            _rewardDeck = new Deck<ConstructionDefinition>(_random);
            foreach (var item in _definition.ConstructionsReward)
                _rewardDeck.Add(item.Key, item.Value);

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
            if (_rewardDeck.IsEmpty())
                return;

            for (int i = 0; i < 3; i++)
            {
                var constrcution = _rewardDeck.Take();
                Hand.Add(constrcution);
            }
        }

    }
}
