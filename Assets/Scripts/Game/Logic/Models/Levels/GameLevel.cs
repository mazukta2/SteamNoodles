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
        public event Action OnTurn = delegate { };

        public PlayerHand Hand { get; private set; }
        public ConstructionsManager Constructions { get; private set; }
        public LevelUnits Units { get; }

        private LevelCrowd _crowd;
        private LevelQueue _queue;

        public Resources Resources { get; }

        private LevelDefinition _settings;
        private SessionRandom _random;

        public GameLevel(LevelDefinition settings, SessionRandom random, IGameTime time, IDefinitions definitions)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _random = random ?? throw new ArgumentNullException(nameof(random));
            if (time == null) throw new ArgumentNullException(nameof(time));

            Hand = new PlayerHand(settings, settings.StartingHand);
            Resources = new Resources();

            var unitSettings = definitions.Get<UnitsSettingsDefinition>();
            Units = new LevelUnits(time);
            _crowd = new LevelCrowd(unitSettings, Units, time, settings, random);
            _queue = new LevelQueue(unitSettings, Units, settings, random, Resources.Points, this);

            Constructions = new ConstructionsManager(definitions.Get<ConstructionsSettingsDefinition>(), _settings, Resources, this);
        }

        protected override void DisposeInner()
        {
            Hand.Dispose();
            Constructions.Dispose();
            Units.Dispose();
            Resources.Dispose();
            _crowd.Dispose();
            _queue.Dispose();
        }

        public void Turn()
        {
            var customer = _settings.BaseCrowdUnits.Keys.ToList()[_random.GetRandom(0, _settings.BaseCrowdUnits.Count - 1)];

            var deck = new Deck<ConstructionDefinition>(_random);
            foreach (var item in customer.ConstructionsReward)
                deck.Add(item.Key, item.Value);

            if (deck.IsEmpty())
                return;

            var constrcution = deck.Take();
            Hand.Add(constrcution);

            OnTurn();
        }
    }
}
