using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Types;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories.Level;
using Game.Assets.Scripts.Game.Logic.Repositories;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class GameLevel : Disposable, IBattleLevel
    {
        public FlowManager Flow { get; }
        public HandService Hand { get; private set; }
        public LevelUnits Units { get; }

        private LevelCustomers _customers;
        private LevelCrowd _crowd;
        public CustomerQueue Queue { get; }

        public Resources Resources { get; }

        private GameLevelRepository _repositories;

        public LevelDefinition Definition { get; private set; }
        public IGameTime Time { get; }
        public BuildingPointsService Points { get; }
        public FieldService Field { get; }
        public BuildingService Building { get; }
        public SchemesService Schemes { get; }


        private IGameRandom _random;

        public GameLevel(LevelDefinition settings, IGameRandom random, IGameTime time, IGameDefinitions definitions)
        {
            // TODO: move repositories outside
            _repositories = new GameLevelRepository();
            IGameLevelPresenterRepository.Default = _repositories;

            Definition = settings ?? throw new ArgumentNullException(nameof(settings));
            _random = random ?? throw new ArgumentNullException(nameof(random));
            Time = time ?? throw new ArgumentNullException(nameof(time));
            Resources = new Resources(definitions.Get<ConstructionsSettingsDefinition>(), time);
            Points = new BuildingPointsService(Resources.Points);
            Field = new FieldService(definitions.Get<ConstructionsSettingsDefinition>().CellSize, settings.PlacementField.Size);
            Schemes = new SchemesService(_repositories.Schemes, _repositories.ConstructionsDeck);
            Hand = new HandService(_repositories.Cards, _repositories.Schemes);

            var unitSettings = definitions.Get<UnitsSettingsDefinition>();

            Units = new LevelUnits(time, definitions.Get<UnitsSettingsDefinition>(), settings, _random);
            Flow = new FlowManager(definitions.Get<ConstructionsSettingsDefinition>(), Definition, random, _repositories.Constructions, Hand, Schemes);
            Building = new BuildingService(_repositories.Constructions, Points, Hand, Field, Flow);
            Flow.OnTurn += TurnManager_OnTurn;
            Flow.OnWaveEnded += TurnManager_OnWaveEnded;

            _customers = new LevelCustomers(_repositories.Constructions, Field, settings, definitions.Get<UnitsSettingsDefinition>(), Resources);
            _crowd = new LevelCrowd(Units, time, settings, random);
            Queue = new CustomerQueue(_customers, Units, _crowd, time, random);

            Resources.Points.OnMaxTargetLevelUp += OnLevelUp;
            Schemes.UpdateSchemes(definitions);
            Schemes.MakeADeck(settings.ConstructionsReward);
            Flow.Start();
        }

        protected override void DisposeInner()
        {
            IGameLevelPresenterRepository.Default = null;
            _repositories.Dispose();

            Resources.Points.OnMaxTargetLevelUp -= OnLevelUp;
            Flow.OnTurn -= TurnManager_OnTurn;
            Flow.OnWaveEnded -= TurnManager_OnWaveEnded;

            Flow.Dispose();
            Units.Dispose();
            Resources.Dispose();
            _crowd.Dispose();
            Queue.Dispose();
            _customers.Dispose();
        }

        private void OnLevelUp()
        {
            Flow.GiveCards();
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
