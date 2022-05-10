using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Common.Creation;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;

namespace Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens
{
    public class MainScreenPrefab : ViewPrefabMock
    {
        public override IView CreateView<T>(ILevelView level, ContainerViewMock container)
        {
            var handContainer = container.Add(new ContainerViewMock(level));
            var prototype = container.Add(new PrototypeViewMock(level, new HandConstructionViewPrefab()));
            var handView = container.Add(new HandView(level, handContainer, prototype));
            return new MainScreenView(level, handView);
        }

        private class HandConstructionViewPrefab : ViewPrefabMock
        {
            public override IView CreateView<T>(ILevelView level, ContainerViewMock container)
            {
                var handTooltipContainer = container.Add(new ContainerViewMock(level));
                var handTooltipPrefab = container.Add(new PrototypeViewMock(level, new HandConstructionTooltipViewPrefab()));

                return new HandConstructionView(level, new ButtonMock(), new ImageMock(), handTooltipContainer, handTooltipPrefab);
            }
        }

        private class HandConstructionTooltipViewPrefab : ViewPrefabMock
        {
            public override IView CreateView<T>(ILevelView level, ContainerViewMock container)
            {
                return new HandConstructionTooltipView(level, new UiText(), new UiText());
            }
        }
    }
}
