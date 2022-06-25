using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Services.Definitions;
using Game.Assets.Scripts.Game.Logic.Services.Game;
using Game.Assets.Scripts.Game.Logic.Services.Session;

namespace Game.Assets.Scripts.Game.Environment
{
    public class Core : Disposable
    {
        public ServiceManager Services { get; private set; }

        public Core(IEngine engine, ILevelsManager levelsManager, IAssets assets, IGameDefinitions definitions, IControls controls,
            ILocalizationManager localizationManager, IGameTime time, bool autoStart = true)
        {
            IGameTime.Default = time;
            IGameRandom.Default = new SessionRandom();
            ILocalizationManager.Default = localizationManager;

            Services = new ServiceManager();
            IPresenterServices.Default = Services;
            IModelServices.Default = Services;

            var levelsRepository = Services.Add(new Repository<Level>());

            Services.Add(new GameService(engine));
            Services.Add(new GameAssetsService(assets));
            Services.Add(new GameControlsService(controls));
            Services.Add(new DefinitionsService(Services, definitions, false)).LoadDefinitions();
            Services.Add(new LevelsService(Services, levelsManager, levelsRepository, 
                definitions.GetList<LevelDefinition>(), 
                definitions.Get<MainDefinition>().StartLevel));

            if (autoStart)
                StartGame();
        }

        protected override void DisposeInner()
        {
            ILocalizationManager.Default = null;
            IGameTime.Default = null;
            IGameRandom.Default = null;
            IPresenterServices.Default = null;
            IModelServices.Default = null;

            Services.Dispose();
            Services = null;
        }

        public void StartGame()
        {
            Services.Get<LevelsService>().StartFirstLevel();
        }
    }
}
