using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;

namespace Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens
{
    public class MainScreenPrefab : ViewCollectionPrefabMock
    {
        public override void Fill(IViewsCollection collection)
        {
            new MainScreenView(collection);
        }
    }
}
