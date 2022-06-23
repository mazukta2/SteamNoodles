﻿using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Common;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Flow.Animations;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using System;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Flow
{
    public class StageTurnService : Disposable, IService
    {
        private readonly IRepository<Construction> _constructions;
        private readonly Field _Field;
        private readonly UnitsCustomerQueueService _unitsCustomerQueueService;
        private readonly SequenceManager _sequence = new SequenceManager();
        private int _turnCounter = 0;

        public StageTurnService(
            IRepository<Construction> constructions,
            Field Field,
            BuildingService buildingService,
            UnitsCustomerQueueService unitsCustomerQueueService)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _Field = Field ?? throw new ArgumentNullException(nameof(Field));
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
                var queueStartingPosition = _Field.GetWorldPosition(construction).X;
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
