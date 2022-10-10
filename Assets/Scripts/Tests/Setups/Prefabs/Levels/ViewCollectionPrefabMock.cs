using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;

namespace Game.Assets.Scripts.Tests.Setups.Prefabs.Levels
{
    public abstract class ViewCollectionPrefabMock : IViewPrefab
    {
        public abstract void Fill(IViews collection);
    }
}
