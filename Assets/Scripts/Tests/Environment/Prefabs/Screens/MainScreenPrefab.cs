using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment.Common;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;
using Game.Assets.Scripts.Tests.Environment.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Environment.Views.Ui.Screens;

namespace Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens
{
    public class MainScreenPrefab : MockViewPrefab
    {
        public override IView CreateView<T>(LevelView level, MockContainerView container)
        {
            var handContainer = container.Add(new MockContainerView(level));
            var prototype = container.Add(new MockPrototypeView(level, new HandConstructionViewPrefab()));
            var handView = container.Add(new HandView(level, handContainer, prototype));
            return new MainScreenView(level, handView, new UiText(), new ProgressBar());
        }

        private class HandConstructionViewPrefab : MockViewPrefab
        {
            public override IView CreateView<T>(LevelView level, MockContainerView container)
            {
                var handTooltipContainer = container.Add(new MockContainerView(level));
                var handTooltipPrefab = container.Add(new MockPrototypeView(level, new HandConstructionTooltipViewPrefab()));

                return new HandConstructionView(level, new ButtonMock(), new ImageMock(), handTooltipContainer, handTooltipPrefab);
            }
        }

        private class HandConstructionTooltipViewPrefab : MockViewPrefab
        {
            public override IView CreateView<T>(LevelView level, MockContainerView container)
            {
                return new HandConstructionTooltipView(level, new UiText());
            }
        }
    }
}
