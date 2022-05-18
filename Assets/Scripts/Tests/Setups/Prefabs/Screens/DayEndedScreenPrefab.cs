using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Common.Creation;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;

namespace Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens
{
    public class DayEndedScreenPrefab : ViewCollectionPrefabMock
    {
        public override void Fill(IViewsCollection collection)
        {
            new DayEndedScreenView(collection);
        }
    }
}
