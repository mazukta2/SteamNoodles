
using Game.Assets.Scripts.Game.Logic.Common.Json.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations;
using Newtonsoft.Json;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Cutscenes.Variations
{
    public class LoadScene : CutsceneStepVariation
    {
        [JsonConverter(typeof(DefinitionsConventer<LevelDefinition>))]
        public LevelDefinition Scene { get; set; }

        public override CutsceneStep Create()
        {
            return new LoadSceneStep(this);
        }

        public override void Validate()
        {
            if (Scene == null)
                throw new System.Exception();
        }
    }
}

