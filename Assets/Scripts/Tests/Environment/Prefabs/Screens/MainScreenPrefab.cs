using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;

namespace Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens
{
    public class MainScreenPrefab : ViewPrefab
    {
        public override View Create<T>(ContainerView conteiner)
        {
            return conteiner.Create((level) =>
            {
                var container = new ContainerView(level);
                var prototype = new PrototypeView(level, new HandConstructionViewPrefab());
                var handView = new HandView(level, container, prototype);
                return new MainScreenView(level, handView);
            });
        }

        private class HandConstructionViewPrefab : ViewPrefab
        {
            public override View Create<T>(ContainerView conteiner)
            {
                return conteiner.Create((level) =>
                {
                    var buttonView = new ButtonView(level);
                    var imageView = new ImageView(level);
                    return new HandConstructionView(level, buttonView, imageView);
                });
            }
        }
    }
}
