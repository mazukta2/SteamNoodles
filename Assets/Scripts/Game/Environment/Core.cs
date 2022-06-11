using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services;
using Game.Assets.Scripts.Game.Logic.Models.Services.Definitions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Controls;

namespace Game.Assets.Scripts.Game.Environment
{
    public class Core : Disposable
    {
        public ServiceManager Services { get; private set; }

        public Core(IEngine engine, ILevelsManager levelsManager, IGameAssets assets, IGameDefinitions definitions, IGameControls controls,
            ILocalizationManager localizationManager, IGameTime time, bool autoStart = true)
        {
            IGameKeysManager.Default = new GameKeysManager();
            IGameAssets.Default = assets;
            IGameControls.Default = controls;
            IGameTime.Default = time;
            IGameRandom.Default = new SessionRandom();
            ILocalizationManager.Default = localizationManager;

            Services = new ServiceManager();
            IPresenterServices.Default = Services;
            IModelServices.Default = Services;

            var levelsRepository = Services.Add(new Repository<Level>());

            Services.Add(new GameService(engine));
            Services.Add(new DefinitionsService(Services, definitions));
            Services.Add(new LevelsService(Services, levelsManager, levelsRepository, 
                definitions.GetList<LevelDefinition>(), 
                definitions.Get<MainDefinition>().StartLevel));

            if (autoStart)
                StartGame();
        }

        protected override void DisposeInner()
        {
            IGameKeysManager.Default = null;
            IGameAssets.Default = null;
            IGameControls.Default.Dispose();
            IGameControls.Default = null;
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
