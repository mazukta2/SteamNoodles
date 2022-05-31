using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Types;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories.Level;
using Game.Assets.Scripts.Game.Logic.Repositories;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class GameLevel : Disposable, IBattleLevel
    {
        public FlowManager Flow { get; }
        public HandService Hand { get; }
        public UnitsService Units { get; }
        private UnitsCrowdService Crowd { get; }
        public UnitsCustomerQueueService Queue { get; }
        public UnitsMovementsService UnitsMovement { get; }
        public Resources Resources { get; }
        public BuildingPointsService Points { get; }
        public FieldService Field { get; }
        public BuildingService Building { get; }
        public SchemesService Schemes { get; }

        public LevelDefinition Definition { get; }
        public IGameTime Time { get; }
        public IGameRandom Random { get; }


        private GameLevelRepository _repositories;


        public GameLevel(LevelDefinition settings, IGameRandom random, IGameTime time, IGameDefinitions definitions)
        {
            Definition = settings ?? throw new ArgumentNullException(nameof(settings));
            Random = random ?? throw new ArgumentNullException(nameof(random));
            Time = time ?? throw new ArgumentNullException(nameof(time));

            // TODO: move repositories outside
            _repositories = new GameLevelRepository();
            IGameLevelPresenterRepository.Default = _repositories;

            var unitSettings = definitions.Get<UnitsSettingsDefinition>();
            var constructionSettings = definitions.Get<ConstructionsSettingsDefinition>();

            Resources = new Resources(constructionSettings, time);
            Points = new BuildingPointsService(Resources.Points);
            Field = new FieldService(constructionSettings.CellSize, settings.PlacementField.Size);
            Schemes = new SchemesService(_repositories.Schemes, _repositories.ConstructionsDeck);
            Hand = new HandService(_repositories.Cards, _repositories.Schemes);
            Units = new UnitsService(_repositories.Units, unitSettings, settings, Random);
            Flow = new FlowManager(constructionSettings, Definition, random, _repositories.Constructions, Hand, Schemes);
            Building = new BuildingService(_repositories.Constructions, Points, Hand, Field, Flow);
            Crowd = new UnitsCrowdService(_repositories.Units, Units, time, settings, random);
            Queue = new UnitsCustomerQueueService(_repositories.Units, Units, Crowd, time, random);
            UnitsMovement = new UnitsMovementsService(_repositories.Units, unitSettings, Time);

            Flow.OnTurn += TurnManager_OnTurn;
            Flow.OnWaveEnded += TurnManager_OnWaveEnded;
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

            UnitsMovement.Dispose();
            Flow.Dispose();
            Units.Dispose();
            Resources.Dispose();
            Crowd.Dispose();
            Queue.Dispose();
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
                Queue.ClearQueue();
                Queue.FreeAll();
            }
        }
    }
}
