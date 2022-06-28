using System;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Services.Units;

namespace Game.Assets.Scripts.Game.Logic.Services.Flow
{
    public class StageTurnService : Disposable, IService
    {
        private readonly IDatabase<Construction> _constructions;
        private readonly UnitsCustomerQueueService _unitsCustomerQueueService;
        private readonly SequenceManager _sequence = new SequenceManager();
        private int _turnCounter = 0;

        public StageTurnService(
            IDatabase<Construction> constructions,
            UnitsCustomerQueueService unitsCustomerQueueService)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _unitsCustomerQueueService = unitsCustomerQueueService ?? throw new ArgumentNullException(nameof(unitsCustomerQueueService));
            _constructions.OnEvent += HandleOnEvent;
        }

        protected override void DisposeInner()
        {
            _constructions.OnEvent -= HandleOnEvent;
            _sequence.Dispose();
        }

        private void Turn()
        {
            if (_turnCounter == 0)
            {
                var construction = _constructions.Get().First();
                var queueStartingPosition = construction.GetWorldPosition().X;
                _unitsCustomerQueueService.SetQueuePosition(queueStartingPosition);
            }

            _unitsCustomerQueueService.TurnQueue();

            _turnCounter++;
        }

        private void HandleOnEvent(Construction construction, IModelEvent e)
        {
            if (e is ConstructionBuiltByPlayerEvent)
                Turn();
        }

    }
}
