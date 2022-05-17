using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class GameLevel : Disposable
    {
        public FlowManager TurnManager { get; }
        public PlayerHand Hand { get; private set; }
        public PlacementField Constructions { get; private set; }
        public LevelUnits Units { get; }

        private LevelCustomers _customers;
        private LevelCrowd _crowd;
        public CustomerQueue Queue { get; }

        public Resources Resources { get; }
        public LevelDefinition Definition { get; private set; }
        public IGameTime Time { get; }

        private SessionRandom _random;

        public GameLevel(LevelDefinition settings, SessionRandom random, IGameTime time, IDefinitions definitions)
        {
            Definition = settings ?? throw new ArgumentNullException(nameof(settings));
            _random = random ?? throw new ArgumentNullException(nameof(random));
            Time = time ?? throw new ArgumentNullException(nameof(time));

            Hand = new PlayerHand(settings, settings.StartingHand);
            Resources = new Resources(definitions.Get<ConstructionsSettingsDefinition>());

            var unitSettings = definitions.Get<UnitsSettingsDefinition>();

            Constructions = new PlacementField(definitions.Get<ConstructionsSettingsDefinition>(), Definition.PlacementField, Resources);
            Units = new LevelUnits(time, definitions.Get<UnitsSettingsDefinition>(), settings, _random);
            TurnManager = new FlowManager(Definition, random, Constructions, Hand);
            TurnManager.OnTurn += TurnManager_OnTurn;
            TurnManager.OnWaveEnded += TurnManager_OnWaveEnded;

            _customers = new LevelCustomers(Constructions, settings, definitions.Get<UnitsSettingsDefinition>(), Resources);
            _crowd = new LevelCrowd(Units, time, settings, random);
            Queue = new CustomerQueue(_customers, Units, _crowd, time, random);

            Resources.Points.OnMaxLevelUp += OnLevelUp;
        }

        protected override void DisposeInner()
        {
            Resources.Points.OnMaxLevelUp -= OnLevelUp;
            TurnManager.OnTurn -= TurnManager_OnTurn;
            TurnManager.OnWaveEnded -= TurnManager_OnWaveEnded;

            TurnManager.Dispose();
            Hand.Dispose();
            Constructions.Dispose();
            Units.Dispose();
            Resources.Dispose();
            _crowd.Dispose();
            Queue.Dispose();
            _customers.Dispose();
        }

        private void OnLevelUp()
        {
            TurnManager.GiveCards(PlayerHand.ConstructionSource.LevelUp);
        }

        private void TurnManager_OnTurn()
        {
            Queue.ServeCustomer();
        }

        private void TurnManager_OnWaveEnded(bool victory)
        {
            if (victory)
                Queue.ServeAll();
            else
            {
                _customers.ClearQueue();
                Queue.FreeAll();
            }
        }

    }
}
