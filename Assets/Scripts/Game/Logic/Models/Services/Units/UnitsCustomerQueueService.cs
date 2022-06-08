using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Customers.Animations;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units.QueueAnimations;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Units;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Units
{
    public class UnitsCustomerQueueService : Disposable, IService
    {
        private readonly IRepository<Unit> _units;
        private readonly UnitsService _unitsService;
        private readonly UnitsCrowdService _crowd;
        private readonly BuildingPointsService _points;
        private readonly CoinsService _coins;
        private readonly IGameTime _time;
        private readonly IGameRandom _random;
        private readonly GameVector3 _firstPositionOffset;
        private readonly float _animationDelay;
        private SequenceManager _sequenceManager = new SequenceManager();
        private QueueSize _queueSize;
        private GameVector3 _queueFirstPosition;
        private float _queuePositionZ;

        public UnitsCustomerQueueService(IRepository<Unit> units, 
            UnitsService unitsService,
            UnitsCrowdService crowd,
            CoinsService coins,
            BuildingPointsService points, 
            IGameTime time, IGameRandom random)
            : this(units, unitsService, crowd, coins, points, time, random, new GameVector3(0, 0, 0))
        {
        }

        public UnitsCustomerQueueService(IRepository<Unit> units, UnitsService unitsService, UnitsCrowdService crowd,
            CoinsService coins,
            BuildingPointsService points,
            IGameTime time, IGameRandom random, GameVector3 firstPositionOffset, float queuePositionZ = 0, float animationDelay = 0)
        {
            _units = units;
            _unitsService = unitsService ?? throw new ArgumentNullException(nameof(unitsService));
            _crowd = crowd ?? throw new ArgumentNullException(nameof(crowd));
            _points = points ?? throw new ArgumentNullException(nameof(points));
            _coins = coins ?? throw new ArgumentNullException(nameof(coins));
            _time = time ?? throw new ArgumentNullException(nameof(time));
            _random = random;
            _firstPositionOffset = firstPositionOffset;
            _animationDelay = animationDelay;
            _queueFirstPosition = new GameVector3(0, 0, 0);
            _queuePositionZ = queuePositionZ;
            _queueSize = new QueueSize(0);

            _points.OnTargetLevelChanged += Points_OnTargetLevelChanged;
        }

        protected override void DisposeInner()
        {
            _sequenceManager.Dispose();
            _points.OnTargetLevelChanged -= Points_OnTargetLevelChanged;
        }

        public GameVector3 GetQueuePosition()
        {
            return _queueFirstPosition;
        }

        public void SetQueuePosition(float position)
        {
            _queueFirstPosition = new GameVector3(position, 0, _queuePositionZ);
        }

        public void TurnQueue()
        {
            _sequenceManager.Add(new ServeFirstCustomer(this, _crowd, Serve));
            UpdateQueue();
        }

        public void ServeAll()
        {
            _sequenceManager.Add(new ServeAllFromQueue(this, _crowd, _random, Serve, _time, _animationDelay));
            _sequenceManager.Add(new AddUnitsToQueue(this, _unitsService, AddToQueue, _time, _animationDelay));
            _sequenceManager.ProcessSteps();
        }

        public void FreeAll()
        {
            _sequenceManager.Add(new FreeAllFromQueue(this, _crowd, _random, RemoveFromQueue));
            _sequenceManager.ProcessSteps();
        }

        public GameVector3 GetPositionFor(int index)
        {
            var offset = GameVector3.Zero;
            if (index == 0)
                offset = GetQueueFirstPositionOffset();

            return GetQueuePosition() + offset + new GameVector3(_unitsService.GetUnitSize(), 0, 0) * index;
        }

        public IReadOnlyCollection<Unit> GetUnits()
        {
            return _units.Get().Where(x => x.State == Unit.BehaviourState.InQueue).AsReadOnly();
        }

        public GameVector3 GetQueueFirstPositionOffset()
        {
            return _firstPositionOffset;
        }

        public void SetQueueSize(QueueSize size)
        {
            _queueSize = size;
            UpdateQueue();
        }

        public QueueSize GetQueueSize()
        {
            return _queueSize;
        }

        public void ClearQueue()
        {
            _queueSize = new QueueSize(0);
        }

        public void Serve(Unit unit)
        {
            RemoveFromQueue(unit);
            _coins.Change(unit.GetCoins());
        }

        public IReadOnlyCollection<Unit> GetQueueUnits()
        {
            return _units.Get().Where(x => x.State == Unit.BehaviourState.InQueue).AsReadOnly();
        }

        public bool IsAnimating()
        {
            return _sequenceManager.IsActive();
        }

        private void UpdateQueue()
        {
            _sequenceManager.Add(new RemoveUnitsFromQueue(this, _crowd, RemoveFromQueue));
            _sequenceManager.Add(new AddUnitsToQueue(this, _unitsService, AddToQueue, _time, _animationDelay));
            _sequenceManager.Add(new MoveUnitsToPositionsInQueue(_units, _unitsService, this));
            _sequenceManager.ProcessSteps();
        }

        private void RemoveFromQueue(Unit unit)
        {
            unit.SetBehaviourState(Unit.BehaviourState.Free);
            _units.Save(unit);
        }

        private void AddToQueue(Unit unit)
        {
            unit.SetBehaviourState(Unit.BehaviourState.InQueue);
            _units.Save(unit);
        }

        private void Points_OnTargetLevelChanged(int changes)
        {
            var change = _queueSize.Value + changes;
            if (change < 0)
                change = 0;

            _queueSize = new QueueSize(change);
        }


    }
}
