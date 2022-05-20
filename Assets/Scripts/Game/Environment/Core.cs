using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
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
    public class Core : Disposable
    {
        public LevelLoading Levels { get; }

        public Core(ILevelsManager levelsManager, IGameAssets assets, IGameDefinitions definitions, IGameControls controls,
            ILocalizationManager localizationManager, IGameTime time, bool autoStart = true)
        {
            IGame.Default = new GameModel();
            IGameKeysManager.Default = new GameKeysManager();
            IGameAssets.Default = assets;
            IGameDefinitions.Default = definitions;
            IGameControls.Default = controls;
            IGameTime.Default = time;
            IGameRandom.Default = new SessionRandom();

            Levels = new LevelLoading(levelsManager, IGame.Default);
            ILocalizationManager.Default = localizationManager;

            if (autoStart)
                IGame.Default.StartNewGame();

            IGame.Default.OnDispose += ModelOnDispose;
        }


        protected override void DisposeInner()
        {
            IGameKeysManager.Default = null;
            IGameAssets.Default = null;
            IGameDefinitions.Default = null;
            IGameControls.Default.Dispose();
            IGameControls.Default = null;
            ILocalizationManager.Default = null;

            IGame.Default.OnDispose -= ModelOnDispose;
            IGame.Default.Exit();
            IGame.Default = null;

            Levels.Dispose();
        }

        private void ModelOnDispose()
        {
            Dispose();
        }
    }
}
