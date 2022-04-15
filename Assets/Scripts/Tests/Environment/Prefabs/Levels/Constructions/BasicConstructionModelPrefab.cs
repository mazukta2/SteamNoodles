using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment.Common;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;
using Game.Assets.Scripts.Tests.Environment.Views.Level;

namespace Game.Assets.Scripts.Tests.Environment.Prefabs.Levels.Constructions
{
    public class BasicConstructionModelPrefab : MockViewPrefab
    {
        public override IView CreateView<T>(LevelView level, MockContainerView container)
        {
            return new ConstructionModelView(level);
        }
    }
}
