using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.ViewPresenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views.Ui.Screens;

namespace Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens
{
    public class MainScreenPrefab : ViewPrefab<MainScreenViewPresenter>
    {
        public override MainScreenViewPresenter Create(ContainerViewPresenter conteiner)
        {
            return conteiner.Create((level) =>
            {
                return new MainScreenViewPresenter(level);
            });
        }
    }
}
