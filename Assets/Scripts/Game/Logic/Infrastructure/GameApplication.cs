using System;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Infrastructure.Flow;
using Game.Assets.Scripts.Game.Logic.Infrastructure.Music;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Controls;

namespace Game.Assets.Scripts.Game.Environment
{
    /// <summary>
    /// Core represent the game itself from starting applications. If core disposed game should shut down.
    /// There is not MVP or game logic here. Only basic infrastructure.
    /// </summary>
    public class GameApplication : Disposable
    {
        public MusicManager Music { get; private set; }
        private LoadingLevel _loading;
        private CurrentLevel _currentLevel;
        private UnityEnviroment _enviroment;
        private readonly ILevelsManager _levelsManager;

        public GameApplication(UnityEnviroment enviroment)
        {
            _enviroment = enviroment;

            var localization = new LocalizationManager(_enviroment.Definitions, "English");
            ILocalizationManager.Default = localization ?? throw new ArgumentNullException();

            IGameKeysManager.Default = new GameKeysManager();
            IGameAssets.Default = new GameAssets(_enviroment.Assets) ?? throw new ArgumentNullException();
            IDefinitions.Default = _enviroment.Definitions ?? throw new ArgumentNullException();
            IGameControls.Default = new GameControls(_enviroment.Controls) ?? throw new ArgumentNullException();
            IGameTime.Default = _enviroment.Time ?? throw new ArgumentNullException();
            IGameRandom.Default = new SessionRandom();
            _levelsManager = _enviroment.Levels;
            Music = new MusicManager(IGameControls.Default, IGameTime.Default);
        }

        protected override void DisposeInner()
        {
            IGameKeysManager.Default = null;
            IGameAssets.Default = null;
            IDefinitions.Default = null;
            IGameControls.Default.Dispose();
            IGameControls.Default = null;
            ILocalizationManager.Default = null;

            Music.Dispose();
            Music = null;

            if (_loading != null && !_loading.IsDisposed)
            {
                _loading.OnLoaded -= WaitLoading;
                _loading.Dispose();
                _loading = null;
            }

            if (_currentLevel != null && !_currentLevel.IsDisposed)
            {
                _currentLevel.Dispose();
                _currentLevel = null;
            }
        }

        public LoadingLevel LoadLevel(LevelDefinition level)
        {
            if (level == null)
                throw new ArgumentNullException(nameof(level));

            if (_currentLevel != null)
            {
                _currentLevel.Dispose();
                _currentLevel = null;
            }

            if (_loading != null && !_loading.IsDisposed)
                throw new Exception("Can't load level before previous is loaded");

            _loading = new LoadingLevel(_levelsManager, level);
            if (_loading.IsLoaded)
                _currentLevel = _loading.GetResult();
            else
                _loading.OnLoaded += WaitLoading;

            return _loading;
        }

        public LoadingLevel Start()
        {
            var level = IDefinitions.Default.Get<MainDefinition>().StartLevel;
            return LoadLevel(level);
        }

        private void WaitLoading(CurrentLevel level)
        {
            _currentLevel = level;

            _loading.OnLoaded -= WaitLoading;
            _loading.Dispose();
            _loading = null;
        }
    }
}
