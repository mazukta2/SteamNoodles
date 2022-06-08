using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
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
    public class StageLevelService : Disposable, IService
    {
        private readonly IModelServices _services;
        private StageLevelRepository _repositories;

        public StageLevelService(StageLevel level)
            : this(level, IModelServices.Default, IGameRandom.Default, IGameTime.Default)
        {

        }

        public StageLevelService(StageLevel level, IModelServices services, IGameRandom random, IGameTime time)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));

            // TODO: move repositories outside
            _repositories = new StageLevelRepository();
            IStageLevelPresenterRepositories.Default = _repositories;

            var coins = services.Add(new CoinsService());
            var points = services.Add(new BuildingPointsService(level, time));
            var field = services.Add(new FieldService(level.CellSize, level.PlacementFieldSize));
            var schemes = services.Add(new SchemesService(_repositories.Schemes, new(_repositories.ConstructionsDeck, random), level.ConstructionsReward));
            var hand = services.Add(new HandService(_repositories.Cards, _repositories.Schemes));
            var unitsTypes = services.Add(new UnitsTypesService(_repositories.UnitTypes, new(_repositories.UnitsDeck, random), level));
            var units = services.Add(new UnitsService(_repositories.Units, random, unitsTypes));

            var constructions = services.Add(new ConstructionsService(_repositories.Constructions, field));
            var building = services.Add(new BuildingService(_repositories.Constructions, constructions, points, hand, field));
            var crowd = services.Add(new UnitsCrowdService(_repositories.Units, units, time, level, random));
            var queue = services.Add(new UnitsCustomerQueueService(_repositories.Units, units, crowd, coins, points, time, random));
            var flow = services.Add(new StageTurnService(_repositories.Constructions, field, building, queue));
            var rewards = services.Add(new RewardsService(level, hand, schemes, points));
            var unitsMovement = services.Add(new UnitsMovementsService(_repositories.Units, time));

            rewards.Start();
        }

        protected override void DisposeInner()
        {
            IStageLevelPresenterRepositories.Default = null;
            _repositories.Dispose();

            _services.Remove<CoinsService>();
            _services.Remove<BuildingPointsService>();
            _services.Remove<FieldService>();
            _services.Remove<SchemesService>();
            _services.Remove<HandService>();
            _services.Remove<UnitsTypesService>();
            _services.Remove<UnitsService>();
            _services.Remove<ConstructionsService>();
            _services.Remove<BuildingService>();
            _services.Remove<UnitsCrowdService>();
            _services.Remove<UnitsCustomerQueueService>();
            _services.Remove<StageTurnService>();
            _services.Remove<RewardsService>();
            _services.Remove<UnitsMovementsService>();
        }

    }
}
