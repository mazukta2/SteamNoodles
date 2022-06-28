using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Entities.Common;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Repositories.Aggregations.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Services.Flow;
using Game.Assets.Scripts.Game.Logic.Services.Resources;
using Game.Assets.Scripts.Game.Logic.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Services.Session;
using Game.Assets.Scripts.Game.Logic.Services.Units;

namespace Game.Assets.Scripts.Game.Logic.Services.Levels
{
    public class StageLevelService : Disposable, IService
    {
        private readonly IModelServices _services;
        private List<IService> _disposables = new List<IService>();

        public StageLevelService(StageLevel level)
            : this(level, IModelServices.Default,IGameRandom.Default, IGameTime.Default)
        {

        }

        public StageLevelService(StageLevel level, IModelServices services, IGameRandom random, IGameTime time)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));

            // databases
            var schemesRep = services.Get<IDatabase<ConstructionScheme>>();
            var unitTypesRep = services.Get<IDatabase<UnitType>>();
            var cardsRep = Add(new Database<ConstructionCard>());
            var constructionsRep = Add(new Database<Construction>());
            var unitsRep = Add(new Database<Unit>());
            var constructionsDeckRep = Add(new SingletonDatabase<Deck<ConstructionScheme>>());
            var unitsDeckRep = Add(new SingletonDatabase<Deck<UnitType>>());
            var field = Add(new SingletonDatabase<Field>(new Field(level.CellSize, level.PlacementFieldSize)));
            var ghostDataBase = new SingletonDatabase<ConstructionGhost>();
            
            // repositories
            var ghost = Add(new GhostRepository(ghostDataBase, constructionsRep, cardsRep, field));
            
            // services
            var coins = Add(new CoinsService());
            var points = Add(new BuildingPointsService(level, time));
            var schemes = Add(new SchemesService(schemesRep, new(constructionsDeckRep, random), level.ConstructionsReward));
            var hand = Add(new HandService(cardsRep));
            var unitsTypes = Add(new UnitsTypesService(unitTypesRep, new(unitsDeckRep, random), level));
            var units = Add(new UnitsService(unitsRep, random, unitsTypes));

            Add(new PointsOnBuildingService(constructionsRep, points));
            Add(new RemoveCardOnBuildingService(constructionsRep, hand));
            var crowd = Add(new UnitsCrowdService(unitsRep, units, time, level, random));
            var queue = Add(new UnitsCustomerQueueService(unitsRep, units, crowd, coins, points, time, random));
            var flow = Add(new StageTurnService(constructionsRep, queue));
            var rewards = Add(new RewardsService(level, hand, schemes, points));
            var unitsMovement = Add(new UnitsMovementsService(unitsRep, time));

            var controls = services.Get<GameControlsService>();
            
            Add(new GhostControlsService(ghost, controls));
            
            rewards.Start();
        }

        protected override void DisposeInner()
        {
            foreach (var item in _disposables)
                _services.Remove(item);
            _disposables.Clear();
        }

        private T Add<T>(T service) where T : IService
        {
            _disposables.Add(service);
            return _services.Add(service);
        }
    }
}
