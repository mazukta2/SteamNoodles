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
using Game.Assets.Scripts.Tests.Definitions;

namespace Game.Assets.Scripts.Tests.Environment.Game
{
    public class FakeGameBuild : IDisposable
    {
        public IViews Views => _currentLevel.GetViews();
        public GameTime Time => _time;
        public ControlsMock Controls => _controls;
        public GameKeysManager Keys => (GameKeysManager)IGameKeysManager.Default;
        public DefinitionsMock Defs => _definitions;

        private ControlsMock _controls;
        private GameTime _time;
        private UnityEnviroment _fakeUnityEnviroment;
        private CurrentLevel _currentLevel;
        private AssetsMock _assets;
        private LevelsManagerMock _levelManager;
        private DefinitionsMock _definitions;

        public FakeGameBuild(AssetsMock assets, DefinitionsMock definitions, LevelsManagerMock levelManager, bool disableAutoload)
        {
            _controls = new ControlsMock();
            _time = new GameTime();
            _assets = assets;
            _levelManager = levelManager;
            _definitions = definitions;

            _fakeUnityEnviroment = new UnityEnviroment(levelManager, assets, definitions, _controls, _time);
            
            IInfrastructure.Default.ConnectEnviroment(_fakeUnityEnviroment, false);
            ILocalizationManager.Default = new LocalizationManagerMock();

            if (!disableAutoload)
            {
                var level = IDefinitions.Default.Get<MainDefinition>().StartLevel;
                _levelManager.Add(level);
                var loading = IInfrastructure.Default.Application.Start();
                _levelManager.FinishLoading();
                _currentLevel = loading.GetResult();
            }
        }

        public void Dispose()
        {
            _assets.ClearPrefabs();

            _fakeUnityEnviroment.Dispose();
            _fakeUnityEnviroment = null;
        }

        private void LoadLevel(LevelDefinition level)
        {
            var loading = IInfrastructure.Default.Application.LoadLevel(level);

            _levelManager.FinishLoading();
            _currentLevel = loading.GetResult();
        }
    }
}
