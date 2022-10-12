using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Cutscenes.Variations
{
    public class PlayAnimation : CutsceneStepVariation
    {
        public string ObjectName { get; set; }
        public string AnimationName { get; set; }
        public float Time { get; set; }

        public override CutsceneStep Create()
        {
            return new PlayAnimationStep(this);
        }

        public override void Validate()
        {
            if (Time < 0)
                throw new Exception();

            if (string.IsNullOrEmpty(ObjectName))
                throw new Exception();

            if (string.IsNullOrEmpty(AnimationName))
                throw new Exception();
        }
    }
}

