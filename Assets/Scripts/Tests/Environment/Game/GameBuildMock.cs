using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Environment.Game
{
    public class GameBuildMock : IDisposable
    {
        public GameEngineMock Engine { get; private set; }
        public Core Core { get; private set; }
        public ControlsMock Controls { get; private set; }

        public GameModel GameModel => Core?.Game;
        public LevelView CurrentLevel => Engine.Levels.GetCurrentLevel();

        public GameKeysManager Keys { get; private set; }

        private List<IDisposable> _toDispose = new List<IDisposable>();
        private AssetsMock _assets;

        public GameBuildMock(Core core, GameEngineMock gameEngine, AssetsMock assets, DefinitionsMock definitions)
        {
            Core = core;
            Engine = gameEngine;

            _assets = assets;
            Controls = new ControlsMock();
            Keys = new GameKeysManager();
            ILocalizationManager.Default = new LocalizationManagerMock();
            IAssets.Default = _assets;
            IDefinitions.Default = definitions;
            IControls.Default = Controls;
            IGameKeysManager.Default = Keys;
        }

        public void Dispose()
        {
            _assets.ClearPrefabs();

            ILocalizationManager.Default = null;
            IGameKeysManager.Default = null;
            IAssets.Default = null;
            IDefinitions.Default = null;
            IControls.Default = null;

            foreach (var item in _toDispose)
                item.Dispose();
            _toDispose.Clear();

            Core.Dispose();
            Engine.Levels.Dispose();

            Core = null;
            Engine = null;
        }

        public void LoadLevel(LevelDefinitionMock loadLevel)
        {
            var session = GameModel.CreateSession();
            var levelLoading = session.LoadLevel(loadLevel);
            GameLevel newLevel = null;

            levelLoading.OnLoaded += HandleOnLoaded;
            Engine.Levels.FinishLoading();
            levelLoading.OnLoaded -= HandleOnLoaded;

            _toDispose.Add(newLevel);
            _toDispose.Add(session);

            void HandleOnLoaded(GameLevel level)
            {
                newLevel = level;
            }
        }

    }
}
