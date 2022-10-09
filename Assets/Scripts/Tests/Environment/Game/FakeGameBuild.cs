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
using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Views.Controls;
using Game.Assets.Scripts.Tests.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Infrastructure.Flow;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Infrastructure;

namespace Game.Assets.Scripts.Tests.Environment.Game
{
    public class FakeGameBuild : IDisposable
    {
        public IViewsCollection LevelCollection => _currentLevel.GetViews();
        public GameTime Time => _time;
        public ControlsMock Controls => _controls;
        public GameKeysManager Keys => (GameKeysManager)IGameKeysManager.Default;

        private ControlsMock _controls;
        private GameTime _time;
        private Core _core;
        private CurrentLevel _currentLevel;
        private AssetsMock _assets;
        private LevelsManagerMock _levelManager;

        public FakeGameBuild(AssetsMock assets, DefinitionsMock definitions, LevelsManagerMock levelManager, bool disableAutoload)
        {
            _controls = new ControlsMock();
            _time = new GameTime();
            _assets = assets;
            _levelManager = levelManager;

            _core = new Core(levelManager, new GameAssets(assets), definitions,
                new GameControls(_controls), new LocalizationManagerMock(), _time);
            IInfrastructure.Default = new DefaultInfrastructure(_core);

            if (!disableAutoload)
            {
                LoadLevel(IGameDefinitions.Default.Get<MainDefinition>().StartLevel);
            }
        }

        public void Dispose()
        {
            _assets.ClearPrefabs();

            IInfrastructure.Default = null;

            _core.Dispose();
            _core = null;
        }

        private void LoadLevel(LevelDefinition level)
        {
            var loading = _core.LoadLevel(level);
            _levelManager.FinishLoading();
            _currentLevel = loading.GetResult();
        }
    }
}
