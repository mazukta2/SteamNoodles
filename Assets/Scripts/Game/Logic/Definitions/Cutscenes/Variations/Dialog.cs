using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Cutscenes.Variations
{
    public class Dialog : CutsceneStepVariation
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public override CutsceneStep Create()
        {
            return new DialogStep(this);
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new Exception();

            if (string.IsNullOrEmpty(Text))
                throw new Exception();
        }
    }
}

