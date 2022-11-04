using System;
using Game.Assets.Scripts.Game.Logic.Common.Json.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.StartMenuLevel;
using Newtonsoft.Json;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels.Variations
{
    public class StartMenuVariation : LevelVariation
    {
        public string StartCutscene { get; set; }
        [JsonConverter(typeof(DefinitionsConventer<LevelDefinition>))]
        public LevelDefinition FirstLevel { get; set; }

        public string StartMusic { get; set; }

        public override (ILevel, IPresenter) CreateModel(LevelDefinition definition, IModels models)
        {
            var model = new StartMenu(this, models, IGameRandom.Default, IGameTime.Default, IDefinitions.Default);
            var presenter = new StartMenuLeverlPresenter(model);
            return (model, presenter);
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(SceneName))
                throw new Exception();

            if (StartCutscene == null)
                throw new System.Exception();
        }
    }
}
