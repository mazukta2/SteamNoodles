using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;

namespace Game.Assets.Scripts.Tests.Setups.Prefabs.Screens
{
    public class BuildScreenPrefab : ViewCollectionPrefabMock
    {
        public override void Fill(IViewsCollection collection)
        {
            new BuildScreenView(collection, new UiWorldText());
        }
    }
}
