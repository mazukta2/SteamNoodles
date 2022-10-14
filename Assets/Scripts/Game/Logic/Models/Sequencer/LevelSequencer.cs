using System;
using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations;

namespace Game.Assets.Scripts.Game.Logic.Models.Sequencer
{
    public class LevelSequencer : Disposable
    {
        public event Action<LevelSequencerStep> OnStepStarted = delegate { };
        public event Action<LevelSequencerStep> OnStepFinished = delegate { };

        private Sequence _steps = new Sequence();

        public LevelSequencer()
        {
            _steps.OnStepStarted += _steps_OnStepStarted;
            _steps.OnStepFinished += _steps_OnStepFinished;
        }

        protected override void DisposeInner()
        {
            _steps.OnStepStarted -= _steps_OnStepStarted;
            _steps.OnStepFinished -= _steps_OnStepFinished;
            _steps.Dispose();
        }

        public void Add(LevelSequencerStep step)
        {
            _steps.Add(step);
        }

        public int GetProcessedStepsAmount() => _steps.GetCurrentStepIndex();

        public void ProcessSteps()
        {
            _steps.ProcessSteps();
        }

        public LevelSequencerStep GetCurrentStep()
        {
            return (LevelSequencerStep)_steps.GetCurrentStep();
        }

        private void _steps_OnStepFinished(BaseSequenceStep obj)
        {
            OnStepFinished((LevelSequencerStep)obj);
        }

        private void _steps_OnStepStarted(BaseSequenceStep obj)
        {
            OnStepStarted((LevelSequencerStep)obj);
        }
    }
}

