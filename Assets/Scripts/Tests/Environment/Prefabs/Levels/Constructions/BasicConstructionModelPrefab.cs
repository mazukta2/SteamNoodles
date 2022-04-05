using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Tests.Environment.Common;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;

namespace Game.Assets.Scripts.Tests.Environment.Prefabs.Levels.Constructions
{
    public class BasicConstructionModelPrefab : TestViewPrefab
    {
        public override View CreateView<T>(ILevel level, TestContainerView container)
        {
            return new ConstructionModelView(level);
        }
    }
}
