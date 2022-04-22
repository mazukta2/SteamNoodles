using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Common.Creation;
using Game.Assets.Scripts.Tests.Views.Level;

namespace Game.Assets.Scripts.Tests.Setups.Prefabs.Levels.Constructions
{
    public class BasicConstructionModelPrefab : ViewPrefabMock
    {
        public override IView CreateView<T>(LevelView level, ContainerViewMock container)
        {
            return new ConstructionModelView(level);
        }
    }
}
