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
    public class TradeLevel : Disposable, ILevel
    {
        public event Action OnStart = delegate { };

        public string SceneName => _definition.SceneName;

        private IGameRandom _random;
        private MainLevelVariation _definition;
        private IModels _models;
        private FlowManager _turnManager;
        private PlayerHand _hand;
        private PlacementField _constructions;
        private LevelUnits _units;
        private IGameTime _time;
        private Resources _resources;
        private CustomerQueue _queue;
        private LevelCustomers _customers;
        private LevelCrowd _crowd;


        public TradeLevel(MainLevelVariation settings, IModels models, IGameRandom random, IGameTime time, IDefinitions definitions)
        {
            _definition = settings ?? throw new ArgumentNullException(nameof(settings));
            if (string.IsNullOrEmpty(_definition.SceneName))
                throw new Exception("No name for scene");
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _time = time ?? throw new ArgumentNullException(nameof(time));
            _models = models;


            _hand = new PlayerHand(settings, settings.StartingHand);
            _resources = new Resources(definitions.Get<ConstructionsSettingsDefinition>(), time);

            var unitSettings = definitions.Get<UnitsSettingsDefinition>();

            _constructions = new PlacementField(definitions.Get<ConstructionsSettingsDefinition>(), _definition.PlacementField, _resources);
            _units = new LevelUnits(time, definitions.Get<UnitsSettingsDefinition>(), settings, _random);
            _turnManager = new FlowManager(definitions.Get<ConstructionsSettingsDefinition>(), _definition, random, _constructions, _hand);
            _turnManager.OnTurn += TurnManager_OnTurn;
            _turnManager.OnWaveEnded += TurnManager_OnWaveEnded;

            _customers = new LevelCustomers(_constructions, settings, definitions.Get<UnitsSettingsDefinition>(), _resources);
            _crowd = new LevelCrowd(_units, time, settings, random);
            _queue = new CustomerQueue(_customers, _units, _crowd, time, random);

            _models.Add(_turnManager);
            _models.Add(_hand);
            _models.Add(_constructions);
            _models.Add(_units);
            _models.Add(_resources.Coins);
            _models.Add(_resources.Points);
            _models.Add(this);

            _resources.Points.OnMaxTargetLevelUp += OnLevelUp;
        }

        protected override void DisposeInner()
        {
            _resources.Points.OnMaxTargetLevelUp -= OnLevelUp;
            _turnManager.OnTurn -= TurnManager_OnTurn;
            _turnManager.OnWaveEnded -= TurnManager_OnWaveEnded;

            _turnManager.Dispose();
            _hand.Dispose();
            _constructions.Dispose();
            _units.Dispose();
            _resources.Dispose();
            _crowd.Dispose();
            _queue.Dispose();
            _customers.Dispose();
        }


        public void Start()
        {
            OnStart();
        }


        private void OnLevelUp()
        {
            _turnManager.GiveCards(PlayerHand.ConstructionSource.LevelUp);
        }

        private void TurnManager_OnTurn()
        {
            _queue.ServeCustomer();
        }

        private void TurnManager_OnWaveEnded(bool victory)
        {
            if (victory)
                _queue.ServeAll();
            else
            {
                _customers.ClearQueue();
                _queue.FreeAll();
            }
        }
    }
}
