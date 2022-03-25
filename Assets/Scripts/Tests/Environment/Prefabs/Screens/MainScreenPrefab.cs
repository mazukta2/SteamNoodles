using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment.Common;

namespace Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens
{
    public class MainScreenPrefab : ViewPrefab
    {
        public override View Create<T>(ContainerView conteiner)
        {
            return conteiner.Create((level) =>
            {
                var container = conteiner.Add(new ContainerView(level));
                var prototype = conteiner.Add(new PrototypeView(level, new HandConstructionViewPrefab()));
                var handView = conteiner.Add(new HandView(level, container, prototype));
                return new MainScreenView(level, handView, new UiText());
            });
        }

        private class HandConstructionViewPrefab : ViewPrefab
        {
            public override View Create<T>(ContainerView conteiner)
            {
                return conteiner.Create((level) =>
                {
                    var buttonView = conteiner.Add(new ButtonView(level));
                    var imageView = conteiner.Add(new ImageView(level));
                    return new HandConstructionView(level, buttonView, imageView);
                });
            }
        }
    }
}
