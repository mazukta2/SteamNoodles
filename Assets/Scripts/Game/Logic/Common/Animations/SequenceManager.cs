using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Common.Animations
{
    public class SequenceManager : Disposable
    {
        private List<BaseSequenceStep> _orders = new List<BaseSequenceStep>();
        private BaseSequenceStep _currentStep;

        protected override void DisposeInner()
        {
            if (_currentStep != null)
                _currentStep.Dispose();

            foreach (var item in _orders)
                item.Dispose();
        }

        public void Add(BaseSequenceStep step)
        {
            _orders.Add(step);
        }

        public void ProcessSteps()
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

        public bool IsActive()
        {
            return _currentStep != null;
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
