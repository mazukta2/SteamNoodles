using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Screens;
using Game.Assets.Scripts.Game.Logic.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Ui;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;

namespace Game.Assets.Scripts.Tests.Setups
{
    public class ScreensSetup : Disposable
    {
        public ScreenService ScreenService { get; set; }
        public ScreenManagerView View { get; set; }
        public AssetsMock Assets { get; set; }

        public ScreensSetup(ViewsCollection viewCollection)
        {
            Assets = new AssetsMock();
            View= new ScreenManagerView(viewCollection);
            ScreenService = new ScreenService(new GameAssetsService(Assets));
            ScreenService.Bind(View);
        }

        public ScreensSetup FullDefault()
        {
            Assets.AddPrefab("Screens/BuildScreen", new DefaultViewPrefab(x => new BuildScreenView(x)));
            Assets.AddPrefab("Screens/MainScreen", new DefaultViewPrefab(x => new MainScreenView(x)));

            return this;
        }
    }
}