using System;
using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Logic.Definitions.Cutscenes.Variations;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Infrastructure;

namespace Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations
{
    public class LoadSceneStep : CutsceneStep
    {
        private GameApplication _application;
        private LevelDefinition _level;

        public LoadSceneStep(LoadScene definition) : this(definition.Scene, IInfrastructure.Default.Application)
        {

        }

        public LoadSceneStep(LevelDefinition sceneName, GameApplication application)
        {
            _application = application ?? throw new ArgumentNullException(nameof(application));
            _level = sceneName ?? throw new ArgumentNullException(nameof(sceneName));
        }

        protected override void DisposeInner()
        {
        }

        public override void Play()
        {
            _application.LoadLevel(_level);
        }
    }
}

