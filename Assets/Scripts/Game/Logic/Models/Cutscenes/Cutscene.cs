using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Time;

namespace Game.Assets.Scripts.Game.Logic.Models
{
    public class Cutscene : Disposable
    {
        private Sequence _steps = new Sequence();

        public Cutscene(IEnumerable<CutsceneStep> steps)
        {

            if (steps.Count() == 0)
                throw new Exception("No steps in definition");

            foreach (var step in steps)
                _steps.Add(step);
            
            _steps.OnFinished += _steps_OnFinished;

            _steps.ProcessSteps();
        }


        public Cutscene(CutsceneDefinition definition)
        {
            if (definition.Steps.Count == 0)
                throw new Exception("No steps in definition");

            for (int i = 0; i < definition.Steps.Count; i++)
            {
                _steps.Add(definition.Steps.ElementAt(i).Create());
            }

            _steps.OnFinished += _steps_OnFinished;

            _steps.ProcessSteps();
        }

        protected override void DisposeInner()
        {
            _steps.OnFinished -= _steps_OnFinished;
            _steps.Dispose();
        }

        private void _steps_OnFinished()
        {
            Dispose();
        }

        public int GetProcessedStepsAmount() => _steps.GetCurrentStepIndex();
    }
}

