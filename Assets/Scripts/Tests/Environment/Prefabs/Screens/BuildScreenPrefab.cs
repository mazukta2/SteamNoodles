using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment.Common;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;

namespace Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens
{
    public class BuildScreenPrefab : TestViewPrefab
    {
        public override View Create<T>(TestContainerView conteiner)
        {
            return conteiner.Create((level) =>
            {
                var button = new ButtonView(level);
                return new BuildScreenView(level, button, new UiText(), new UiText(), new ProgressBar());
            });
        }
    }
}
