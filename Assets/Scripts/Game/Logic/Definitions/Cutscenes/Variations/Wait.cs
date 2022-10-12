using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Cutscenes.Variations
{
    public class Wait : CutsceneStepVariation
    {
        public float Time { get; set; }

        public override CutsceneStep Create()
        {
            return new WaitStep(this);
        }

        public override void Validate()
        {
            if (Time < 0)
                throw new Exception();
        }
    }
}

