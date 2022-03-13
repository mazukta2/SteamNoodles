using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;

namespace Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens
{
    public class BuildScreenPrefab : ViewPrefab
    {
        public override object Create<T>(ContainerView conteiner)
        {
            return conteiner.Create((level) =>
            {
                var button = new ButtonView(level);
                return new BuildScreenView(level, button);
            });
        }
    }
}
