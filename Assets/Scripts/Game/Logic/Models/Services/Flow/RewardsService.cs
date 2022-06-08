using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers.Animations;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services.Common;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Flow.Animations;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Flow
{
    public class RewardsService : Disposable, IService
    {
        private readonly StageLevel _level;
        private readonly HandService _hand;
        private readonly BuildingPointsService _points;
        private readonly int _giveCardsAmount;
        private readonly SchemesService _schemesService;
        private readonly SequenceManager _sequence = new SequenceManager();

        public RewardsService(
            StageLevel level, 
            HandService handService, 
            SchemesService schemesService,
            BuildingPointsService buildingPointsService,
            int giveCardsAmount = 3)
        {
            _level = level ?? throw new ArgumentNullException(nameof(level));
            _hand = handService ?? throw new ArgumentNullException(nameof(handService));
            _points = buildingPointsService ?? throw new ArgumentNullException(nameof(buildingPointsService));
            _giveCardsAmount = giveCardsAmount;
            _schemesService = schemesService ?? throw new ArgumentNullException(nameof(schemesService));

            _points.OnMaxTargetLevelUp += HandleOnLevelUp;
        }

        protected override void DisposeInner()
        {
            _points.OnMaxTargetLevelUp -= HandleOnLevelUp;
            _sequence.Dispose();
        }

        public void Start()
        {
            foreach (var scheme in _level.StartingSchemes)
                _hand.Add(scheme);
        }

        public void GiveCards()
        {
            for (int i = 0; i < _giveCardsAmount; i++)
            {
                var constrcution = _schemesService.TakeRandom();
                _hand.Add(constrcution);
            }
        }

        private void HandleOnLevelUp()
        {
            GiveCards();
        }
    }
}
