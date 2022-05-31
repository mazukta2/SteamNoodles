using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Customers.Animations;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units.QueueAnimations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Units
{
    public class UnitsCustomerQueueService : Disposable
    {
        private readonly IRepository<Unit> _units;
        private readonly UnitsService _unitsController;
        private readonly UnitsCrowdService _crowd;
        private readonly IGameTime _time;
        private readonly IGameRandom _random;
        private readonly GameVector3 _firstPositionOffset;
        private readonly float _animationDelay;
        private SequenceManager _sequenceManager = new SequenceManager();
        private int _queueSize;

        public UnitsCustomerQueueService(IRepository<Unit> units, UnitsService unitsService, UnitsCrowdService crowd, IGameTime time, IGameRandom random)
        {
            _units = units;
            _unitsController = unitsService ?? throw new ArgumentNullException(nameof(unitsService));
            _crowd = crowd ?? throw new ArgumentNullException(nameof(crowd));
            _time = time ?? throw new ArgumentNullException(nameof(time));
            _random = random;
            //_resources.Points.OnTargetLevelChanged += Points_OnTargetLevelChanged;
        }


        public UnitsCustomerQueueService(IRepository<Unit> units, UnitsService unitsService, UnitsCrowdService crowd, 
            IGameTime time, IGameRandom random, GameVector3 firstPositionOffset, float animationDelay = 0)
        {
            _units = units;
            _unitsController = unitsService ?? throw new ArgumentNullException(nameof(unitsService));
            _crowd = crowd ?? throw new ArgumentNullException(nameof(crowd));
            _time = time ?? throw new ArgumentNullException(nameof(time));
            _random = random;
            _firstPositionOffset = firstPositionOffset;
            _animationDelay = animationDelay;
            //_resources.Points.OnTargetLevelChanged += Points_OnTargetLevelChanged;
        }

       

        protected override void DisposeInner()
        {
            _sequenceManager.Dispose();
            //_resources.Points.OnTargetLevelChanged -= Points_OnTargetLevelChanged;
        }

        //public float SpawnAnimationDelay => _unitsSettings.SpawnAnimationDelay;

        public GameVector3 GetQueueFirstPosition()
        {
            throw new Exception();
            //var construction = _constructions.Get().First();
            //var queueStartingPosition = _fieldPositionService.GetWorldPosition(construction).X;
            //return new GameVector3(queueStartingPosition, 0, _levelDefinition.QueuePosition.Z);
        }

        public void ServeCustomer()
        {
            var targetSize = GetQueueSize();
            if (targetSize < 0)
                throw new ArgumentException(nameof(targetSize));

            _sequenceManager.Add(new ServeFirstCustomer(this, _crowd, RemoveFromQueue, ServeUnit));
            _sequenceManager.Add(new RemoveUnitsFromQueue(this, _crowd, RemoveFromQueue));
            _sequenceManager.Add(new AddUnitsToQueue(this, _unitsController, AddToQueue, _time, _animationDelay));
            _sequenceManager.Add(new MoveUnitsToPositionsInQueue(_units, _unitsController, this));

            _sequenceManager.ProcessSteps();
        }

        public void ServeAll()
        {
            _sequenceManager.Add(new ServeAllFromQueue(this, _crowd, _random, RemoveFromQueue, ServeUnit, _time, _animationDelay));
            _sequenceManager.Add(new AddUnitsToQueue(this, _unitsController, AddToQueue, _time, _animationDelay));
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

            return GetQueueFirstPosition() + offset + new GameVector3(_unitsController.GetUnitSize(), 0, 0) * index;
        }

        public IReadOnlyCollection<Unit> GetUnits()
        {
            return _units.Get().Where(x => x.State == Unit.BehaviourState.InQueue).AsReadOnly();
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

        private void ServeUnit(Unit unit)
        {
            Serve(unit);
        }

        public GameVector3 GetQueueFirstPositionOffset()
        {
            return _firstPositionOffset;
        }

        public void SetQueueSize(int size)
        {
            _queueSize = size;
        }

        public int GetQueueSize()
        {
            return _queueSize;
        }

        public void ClearQueue()
        {
            _queueSize = 0;
        }

        private void Points_OnTargetLevelChanged(int changes)
        {
            _queueSize += changes;
            if (_queueSize < 0)
                _queueSize = 0;
        }

        public void Serve(Unit unit)
        {
            //_resources.Coins.Change(_unitsSettings.BaseCoins);
        }

        public IReadOnlyCollection<Unit> GetQueueUnits()
        {
            return _units.Get().Where(x => x.State == Unit.BehaviourState.InQueue).AsReadOnly();
        }

    }
}
