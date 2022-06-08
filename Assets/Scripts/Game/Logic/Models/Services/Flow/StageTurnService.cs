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
    public class StageTurnService : Disposable, IService
    {
        private readonly IRepository<Construction> _constructions;
        private readonly FieldService _fieldService;
        private readonly BuildingService _buildingService;
        private readonly UnitsCustomerQueueService _unitsCustomerQueueService;
        private readonly SequenceManager _sequence = new SequenceManager();
        private int _turnCounter = 0;

        public StageTurnService(
            IRepository<Construction> constructions,
            FieldService fieldService,
            BuildingService buildingService,
            UnitsCustomerQueueService unitsCustomerQueueService)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
            _buildingService = buildingService ?? throw new ArgumentNullException(nameof(buildingService));
            _unitsCustomerQueueService = unitsCustomerQueueService ?? throw new ArgumentNullException(nameof(unitsCustomerQueueService));
            _buildingService.OnBuild += HandleOnBuild;
        }

        protected override void DisposeInner()
        {
            _buildingService.OnBuild -= HandleOnBuild;
            _sequence.Dispose();
        }

        private void Turn()
        {
            if (_turnCounter == 0)
            {
                var construction = _constructions.Get().First();
                var queueStartingPosition = _fieldService.GetWorldPosition(construction).X;
                _unitsCustomerQueueService.SetQueuePosition(queueStartingPosition);
            }

            _unitsCustomerQueueService.TurnQueue();

            _turnCounter++;
        }

        private void HandleOnBuild(Construction obj)
        {
            Turn();
        }

    }
}
