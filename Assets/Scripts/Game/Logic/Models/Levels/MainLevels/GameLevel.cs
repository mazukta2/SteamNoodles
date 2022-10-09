using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels.Variations
{
    public class GameLevel : Disposable, IMainLevel
    {
        public event Action OnStart = delegate { };

        public FlowManager TurnManager { get; }
        public PlayerHand Hand { get; private set; }
        public PlacementField Constructions { get; private set; }
        public LevelUnits Units { get; }

        private LevelCustomers _customers;
        private LevelCrowd _crowd;
        public CustomerQueue Queue { get; }

        public Resources Resources { get; }
        public IGameTime Time { get; }

        public string SceneName => _definition.SceneName;

        private IGameRandom _random;
        private MainLevelVariation _definition;

        public GameLevel(MainLevelVariation settings, IGameRandom random, IGameTime time, IGameDefinitions definitions)
        {
            _definition = settings ?? throw new ArgumentNullException(nameof(settings));
            _random = random ?? throw new ArgumentNullException(nameof(random));
            Time = time ?? throw new ArgumentNullException(nameof(time));

            Hand = new PlayerHand(settings, settings.StartingHand);
            Resources = new Resources(definitions.Get<ConstructionsSettingsDefinition>(), time);

            var unitSettings = definitions.Get<UnitsSettingsDefinition>();

            Constructions = new PlacementField(definitions.Get<ConstructionsSettingsDefinition>(), _definition.PlacementField, Resources);
            Units = new LevelUnits(time, definitions.Get<UnitsSettingsDefinition>(), settings, _random);
            TurnManager = new FlowManager(definitions.Get<ConstructionsSettingsDefinition>(), _definition, random, Constructions, Hand);
            TurnManager.OnTurn += TurnManager_OnTurn;
            TurnManager.OnWaveEnded += TurnManager_OnWaveEnded;

            _customers = new LevelCustomers(Constructions, settings, definitions.Get<UnitsSettingsDefinition>(), Resources);
            _crowd = new LevelCrowd(Units, time, settings, random);
            Queue = new CustomerQueue(_customers, Units, _crowd, time, random);

            Resources.Points.OnMaxTargetLevelUp += OnLevelUp;
        }

        protected override void DisposeInner()
        {
            Resources.Points.OnMaxTargetLevelUp -= OnLevelUp;
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


        public void Start()
        {
            OnStart();
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
