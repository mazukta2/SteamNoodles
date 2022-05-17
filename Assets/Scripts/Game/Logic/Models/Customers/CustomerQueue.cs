using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Customers.Animations;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public class CustomerQueue : Disposable
    {
        public IReadOnlyCollection<Unit> Units => _queue.AsReadOnly();

        private readonly ICustomers _customers;
        private IUnits _unitsController;
        private ICrowd _crowd;
        private readonly IGameTime _time;
        private List<Unit> _queue = new List<Unit>();
        private List<BaseQueueStep> _orders = new List<BaseQueueStep>();
        private BaseQueueStep _currentStep;

        public CustomerQueue(ICustomers customers, IUnits unitsController, ICrowd crowd, IGameTime time)
        {
            _customers = customers ?? throw new ArgumentNullException(nameof(customers));
            _unitsController = unitsController ?? throw new ArgumentNullException(nameof(unitsController));
            _crowd = crowd ?? throw new ArgumentNullException(nameof(crowd));
            _time = time ?? throw new ArgumentNullException(nameof(time));
        }

        protected override void DisposeInner()
        {
            if (_currentStep != null)
                _currentStep.Dispose();

            foreach (var item in _orders)
                item.Dispose();
        }

        public void ServeCustomer()
        {
            var targetSize = _customers.GetQueueSize();
            if (targetSize < 0)
                throw new ArgumentException(nameof(targetSize));

            var currentSize = _queue.Count;
            if (currentSize > 0)
            {
                _orders.Add(new ServeFirstCustomer(this, _crowd, RemoveFromQueue));
                currentSize--;
            }

            // remove units
            if (targetSize < currentSize)
            {
                var count = currentSize - targetSize;
                for (int i = _queue.Count - 1; i >= _queue.Count - 1 - count; i--)
                    _orders.Add(new RemoveUnitFromQueue(_queue[i], _crowd, RemoveFromQueue));
            }

            // add new units
            if (targetSize > currentSize)
            {
                var count = targetSize - currentSize;
                for (int i = 0; i < count; i++)
                {
                    _orders.Add(new AddDelay(_time, _customers.SpawnAnimationDelay));
                    _orders.Add(new AddUnitToQueue(this, _customers, _unitsController, AddToQueue));
                }
            }

            _orders.Add(new MoveUnitsToPositionsInQueue(this, _customers));

            ProcessSteps();
        }

        public GameVector3 GetPositionFor(int index)
        {
            var offset = GameVector3.Zero;
            if (index == 0)
                offset = _customers.GetQueueFirstPositionOffset();

            return _customers.GetQueueFirstPosition() + offset + new GameVector3(_unitsController.GetUnitSize(), 0, 0) * index;
        }

        private void RemoveFromQueue(Unit unit)
        {
            _queue.Remove(unit);
        }

        private void AddToQueue(Unit unit)
        {
            _queue.Add(unit);
        }

        private void ProcessSteps()
        {
            if (_currentStep != null)
                return;

            if (_orders.Count == 0)
                return;

            _currentStep = _orders.First();
            _orders.RemoveAt(0);

            _currentStep.OnFinished += FinishCurrentStep;
            _currentStep.Play();
        }

        private void FinishCurrentStep()
        {
            _currentStep.OnFinished -= FinishCurrentStep;
            _currentStep.Dispose();
            _currentStep = null;
            ProcessSteps();
        }
    }
}
