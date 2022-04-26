using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Common.Creation;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;

namespace Game.Assets.Scripts.Tests.Setups.Prefabs.Screens
{
    public class BuildScreenPrefab : ViewPrefabMock
    {
        public override IView CreateView<T>(LevelView level, ContainerViewMock container)
        {
            return new BuildScreenView(level, new ButtonMock(), new UiWorldText(), new UiText(), new ProgressBar());
        }
    }
}
