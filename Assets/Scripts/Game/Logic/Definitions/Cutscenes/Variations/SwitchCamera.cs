using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Cutscenes.Variations
{
    public class SwitchCamera : CutsceneStepVariation
    {
        public string CameraName { get; set; }
        public float Time { get; set; }

        public override CutsceneStep Create()
        {
            return new SwitchCameraStep(this);
        }

        public override void Validate()
        {
            if (Time < 0)
                throw new Exception();

            if (string.IsNullOrEmpty(CameraName))
                throw new Exception();
        }
    }
}

