using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Cutscenes.Variations
{
    public class StopMusic : CutsceneStepVariation
    {

        public override CutsceneStep Create()
        {
            return new StopMusicStep(this);
        }

        public override void Validate()
        {
        }
    }
}

