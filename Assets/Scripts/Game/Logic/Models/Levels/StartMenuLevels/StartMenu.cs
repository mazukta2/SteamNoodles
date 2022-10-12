using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels.Variations;
using static Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions.HandPresenter;
using System.Reflection;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels.Variations
{
    public class StartMenu : Disposable, ILevel
    {
        private StartMenuVariation _definition;
        private IModels _models;

        public StartMenu(StartMenuVariation settings, IModels models, IGameRandom random, IGameTime time, IGameDefinitions definitions)
        {
            _definition = settings;
            if (string.IsNullOrEmpty(_definition.SceneName))
                throw new Exception("No name for scene");

            _models = models;
            _models.Add(this);
        }

        public string SceneName => _definition.SceneName;

        public void Start()
        {
        }

        protected override void DisposeInner()
        {
        }

        public void StartCutscene()
        {
            new Cutscene(_definition.StartCutscene);
        }
    }
}
