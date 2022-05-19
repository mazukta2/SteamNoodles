using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Environment.Game
{
    public class TestsGameBuild : IDisposable
    {
        public LevelsManagerMock Levels { get; private set; }
        public Core Core { get; private set; }
        public ControlsMock Controls { get; private set; }

        public GameModel GameModel => Core?.Game;
        public IViewsCollection LevelCollection => Core.Levels.Views;

        public GameKeysManager Keys => (GameKeysManager)IGameKeysManager.Default;
        public GameTime Time { get; private set; }

        private AssetsMock _assets;

        public TestsGameBuild(AssetsMock assets, DefinitionsMock definitions, LevelsManagerMock levelManager, bool disableAutoload)
        {
            Controls = new ControlsMock();
            Levels = levelManager;
            _assets = assets;

            Time = new GameTime();
            Core = new Core(Levels, new GameAssets(assets), definitions, new GameControls(Controls), new LocalizationManagerMock(), Time, !disableAutoload);
            if (!disableAutoload)
                Levels.FinishLoading();
        }

        public void Dispose()
        {
            _assets.ClearPrefabs();
            
            Core.Dispose();
            Core = null;
        }

        public void LoadLevel(LevelDefinition loadLevel)
        {
            Core.Game.SetLevel(loadLevel);
            Levels.FinishLoading();
        }
    }
}
