using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Sequencer;
using Game.Assets.Scripts.Game.Logic.Models.Time;

namespace Game.Assets.Scripts.Game.Logic.Models
{
    public class Cutscene : Disposable
    {
        private LevelSequencer _levelSequencer;
        private List<CutsceneStep> _steps = new List<CutsceneStep>();

        public Cutscene(LevelSequencer levelSequencer, IEnumerable<CutsceneStep> steps)
        {
            _levelSequencer = levelSequencer;
            Init(_steps);
        }

        protected Cutscene(LevelSequencer levelSequencer)
        {
            _levelSequencer = levelSequencer;
        }
        
        protected void Init(IEnumerable<CutsceneStep> steps)
        {
            if (steps.Count() == 0)
                throw new Exception("No steps in definition");
            
            foreach (var step in steps)
            {
                _steps.Add(step);
                _levelSequencer.Add(step);
            }

            _levelSequencer.ProcessSteps();

            _levelSequencer.OnStepFinished += _step_OnFinished;
        }


        public Cutscene(LevelSequencer levelSequencer, CutsceneDefinition definition)
        {
            if (definition.Steps.Count == 0)
                throw new Exception("No steps in definition");

            _levelSequencer = levelSequencer;

            for (int i = 0; i < definition.Steps.Count; i++)
            {
                var step = definition.Steps.ElementAt(i).Create();
                _steps.Add(step);
                _levelSequencer.Add(step);
            }

            _levelSequencer.OnStepFinished += _step_OnFinished;

            _levelSequencer.ProcessSteps();
        }

        protected override void DisposeInner()
        {
            _levelSequencer.OnStepFinished -= _step_OnFinished;
        }

        private void _step_OnFinished(LevelSequencerStep step)
        {
            if (!_steps.All(x => x.IsDisposed))
                return;

            Dispose();
        }
    }
}

