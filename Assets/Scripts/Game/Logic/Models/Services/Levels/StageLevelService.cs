using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Common;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Flow;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories.Level;
using Game.Assets.Scripts.Game.Logic.Repositories;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Levels
{
    public class StageLevelService : Disposable, IStageLevelService
    {
        public StageTurnService Flow { get; }
        public RewardsService Rewards { get; }
        public ConstructionsService Constructions { get; }
        public HandService Hand { get; }
        public UnitsTypesService UnitsTypes { get; }
        public UnitsService Units { get; }
        public UnitsCrowdService Crowd { get; }
        public UnitsCustomerQueueService Queue { get; }
        public UnitsMovementsService UnitsMovement { get; }
        public BuildingPointsService Points { get; }
        public FieldService Field { get; }
        public BuildingService Building { get; }
        public SchemesService Schemes { get; }
        public CoinsService Coins { get; }

        public LevelDefinition Definition { get; }
        public IGameTime Time { get; }
        public IGameRandom Random { get; }

        private StageLevelRepository _repositories;

        public StageLevelService(LevelDefinition settings, IGameRandom random, IGameTime time, IGameDefinitions definitions)
        {
            Definition = settings ?? throw new ArgumentNullException(nameof(settings));
            Random = random ?? throw new ArgumentNullException(nameof(random));
            Time = time ?? throw new ArgumentNullException(nameof(time));
            var level = new StageLevel(settings);

            // TODO: move repositories outside
            _repositories = new StageLevelRepository();
            IStageLevelPresenterRepositories.Default = _repositories;

            var unitSettings = definitions.Get<UnitsSettingsDefinition>();
            var constructionSettings = definitions.Get<ConstructionsSettingsDefinition>();

            Coins = new CoinsService();
            Points = new BuildingPointsService(constructionSettings, time);
            Field = new FieldService(constructionSettings.CellSize, settings.PlacementField.Size);
            Schemes = new SchemesService(definitions, settings.ConstructionsReward,
                _repositories.Schemes, new (_repositories.ConstructionsDeck, Random));
            Hand = new HandService(_repositories.Cards, _repositories.Schemes);
            UnitsTypes = new UnitsTypesService(settings, unitSettings, _repositories.UnitTypes,
                new(_repositories.UnitsDeck, Random));
            Units = new UnitsService(_repositories.Units,  Random, UnitsTypes);

            Flow = new StageTurnService(_repositories.Constructions, Field, Building, Queue);
            Rewards = new RewardsService(level, Hand, Schemes, Points);
            Constructions = new ConstructionsService(_repositories.Constructions, Field);
            Building = new BuildingService(_repositories.Constructions, Constructions, Points, Hand, Field);
            Crowd = new UnitsCrowdService(_repositories.Units, Units, time, settings, random);
            Queue = new UnitsCustomerQueueService(_repositories.Units, Units, Crowd, Points, time, random);
            UnitsMovement = new UnitsMovementsService(_repositories.Units, Time);

            Rewards.Start();
        }

        protected override void DisposeInner()
        {
            IStageLevelPresenterRepositories.Default = null;
            _repositories.Dispose();

            UnitsMovement.Dispose();
            Flow.Dispose();
            Units.Dispose();
            Crowd.Dispose();
            Queue.Dispose();
        }

    }
}
