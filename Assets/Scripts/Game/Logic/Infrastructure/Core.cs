using System;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Infrastructure.Flow;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
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
    public class Core : Disposable
    {
        private LoadingLevel _loading;
        private CurrentLevel _currentLevel;
        private readonly ILevelsManager _levelsManager;

        public Core(ILevelsManager levelsManager, IGameAssets assets, IGameDefinitions definitions, IGameControls controls,
            ILocalizationManager localizationManager, IGameTime time)
        {
            IGameKeysManager.Default = new GameKeysManager();
            IGameAssets.Default = assets ?? throw new ArgumentNullException();
            IGameDefinitions.Default = definitions ?? throw new ArgumentNullException();
            IGameControls.Default = controls ?? throw new ArgumentNullException();
            IGameTime.Default = time ?? throw new ArgumentNullException();
            IGameRandom.Default = new SessionRandom();
            ILocalizationManager.Default = localizationManager ?? throw new ArgumentNullException();
            _levelsManager = levelsManager;
        }


        protected override void DisposeInner()
        {
            IGameKeysManager.Default = null;
            IGameAssets.Default = null;
            IGameDefinitions.Default = null;
            IGameControls.Default.Dispose();
            IGameControls.Default = null;
            ILocalizationManager.Default = null;

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

        private void WaitLoading(CurrentLevel level)
        {
            _currentLevel = level;

            _loading.OnLoaded -= WaitLoading;
            _loading.Dispose();
            _loading = null;
        }
    }
}
