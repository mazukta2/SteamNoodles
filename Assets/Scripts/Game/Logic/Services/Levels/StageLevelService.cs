﻿using System;
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
using Game.Assets.Scripts.Game.Logic.Services.Constructions.Ghost;
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

            var schemesRep = services.Get<IRepository<ConstructionScheme>>();
            var unitTypesRep = services.Get<IRepository<UnitType>>();
            var cardsRep = Add(new Repository<ConstructionCard>());
            var constructionsRep = Add(new Repository<Construction>());
            var unitsRep = Add(new Repository<Unit>());
            var constructionsDeckRep = Add(new SingletonRepository<Deck<ConstructionScheme>>());
            var unitsDeckRep = Add(new SingletonRepository<Deck<UnitType>>());
            var field = Add(new SingletonRepository<Field>(new Field(level.CellSize, level.PlacementFieldSize)));

            
            var ghost = Add(new GhostRepository());
            
            
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
            var ghostRep = Add(new SingletonRepository<ConstructionGhost>());
            
            Add(new GhostMovingService(ghostRep, field, controls));
            Add(new GhostRotatingService(ghostRep, controls));
            Add(new GhostBuildingService(ghost, controls));
            
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
