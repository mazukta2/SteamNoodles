using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Common.Services.Events;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Common;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Flow;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Levels
{
    public class StageLevelService : Disposable, IService
    {
        private readonly IModelServices _services;

        public StageLevelService(StageLevel level)
            : this(level, IModelServices.Default, IEvents.Default, ICommands.Default, IGameRandom.Default, IGameTime.Default)
        {

        }

        public StageLevelService(StageLevel level, IModelServices services, IEvents events, ICommands commands, IGameRandom random, IGameTime time)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));

            var schemesRep = services.Get<IRepository<ConstructionScheme>>();
            var unitTypesRep = services.Get<IRepository<UnitType>>();

            var cardsRep = services.Add(new Repository<ConstructionCard>(events));
            var constructionsRep = services.Add(new Repository<Construction>(events));
            var unitsRep = services.Add(new Repository<Unit>(events));
            var constructionsDeckRep = services.Add(new SingletonRepository<Deck<ConstructionScheme>>(events));
            var unitsDeckRep = services.Add(new SingletonRepository<Deck<UnitType>>(events));

            var coins = services.Add(new CoinsService());
            var points = services.Add(new BuildingPointsService(level, time));
            var field = services.Add(new FieldService(level.CellSize, level.PlacementFieldSize));
            var schemes = services.Add(new SchemesService(schemesRep, new(constructionsDeckRep, random), level.ConstructionsReward));
            var hand = services.Add(new HandService(cardsRep, schemesRep));
            var unitsTypes = services.Add(new UnitsTypesService(unitTypesRep, new(unitsDeckRep, random), level));
            var units = services.Add(new UnitsService(unitsRep, random, unitsTypes));

            var constructions = services.Add(new ConstructionsService(constructionsRep, field));
            var building = services.Add(new BuildingService(constructionsRep, constructions, points, hand, field));
            var crowd = services.Add(new UnitsCrowdService(unitsRep, units, time, level, random));
            var queue = services.Add(new UnitsCustomerQueueService(unitsRep, units, crowd, coins, points, time, random));
            var flow = services.Add(new StageTurnService(constructionsRep, field, building, queue));
            var rewards = services.Add(new RewardsService(level, hand, schemes, points));
            var unitsMovement = services.Add(new UnitsMovementsService(unitsRep, time));
            var buildingMode = services.Add(new BuildingModeService(events));

            services.Add(new FieldRequestsService(field, constructions, buildingMode, events));
            services.Add(new ConstructionsRequestsService(constructionsRep, buildingMode, field, commands, IGameAssets.Default));
            
            rewards.Start();
        }

        protected override void DisposeInner()
        {
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
            _services.Remove<BuildingModeService>();

            _services.Remove<FieldRequestsService>();
            _services.Remove<ConstructionsRequestsService>();

            _services.Remove<Repository<ConstructionCard>>();
            _services.Remove<Repository<Construction>>();
            _services.Remove<Repository<Unit>>();
            _services.Remove<SingletonRepository<Deck<ConstructionScheme>>>();
            _services.Remove<SingletonRepository<Deck<UnitType>>>();
        }

    }
}
