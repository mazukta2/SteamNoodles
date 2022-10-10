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

namespace Game.Assets.Scripts.Game.Logic.Models.Levels.Variations
{
    public class StartMenu : Disposable, ILevel
    {
        private StartMenuVariation _definition;

        public StartMenu(StartMenuVariation settings, IGameRandom random, IGameTime time, IGameDefinitions definitions)
        {
            _definition = settings;
            if (string.IsNullOrEmpty(_definition.SceneName))
                throw new Exception("No name for scene");
        }

        public string SceneName => _definition.SceneName;

        public void Start()
        {
        }

        protected override void DisposeInner()
        {
        }
    }
}
