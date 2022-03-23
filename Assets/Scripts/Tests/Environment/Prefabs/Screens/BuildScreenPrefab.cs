using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment.Common;

namespace Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens
{
    public class BuildScreenPrefab : ViewPrefab
    {
        public override View Create<T>(ContainerView conteiner)
        {
            return conteiner.Create((level) =>
            {
                var button = new ButtonView(level);
                return new BuildScreenView(level, button, new UiText());
            });
        }
    }
}
