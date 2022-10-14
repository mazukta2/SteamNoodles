using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Cutscenes.Variations;
using Game.Assets.Scripts.Game.Logic.Models.Time;

namespace Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations
{
    public class DialogStep : CutsceneStep
    {

        public DialogStep(Dialog definition) : this()
        {

        }

        public DialogStep()
        {

        }

        protected override void DisposeInner()
        {
        }

        public override void Play()
        {
        }

        public void Process()
        {
            FireOnFinished();
        }
    }
}

