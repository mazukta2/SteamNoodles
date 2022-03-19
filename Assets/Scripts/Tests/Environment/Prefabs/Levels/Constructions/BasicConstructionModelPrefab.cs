using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Tests.Environment.Common;

namespace Game.Assets.Scripts.Tests.Environment.Prefabs.Levels.Constructions
{
    public class BasicConstructionModelPrefab : ViewPrefab
    {
        public override View Create<T>(ContainerView conteiner)
        {
            return conteiner.Create((level) =>
            {
                return new ConstructionModelView(level);
            });
        }
    }
}
