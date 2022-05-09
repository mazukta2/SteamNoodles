using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;

namespace Game.Assets.Scripts.Tests.Views.Common.Creation
{
    public class PrototypeViewMock : View, IViewPrefab
    {
        private ViewPrefabMock _prefab;

        public PrototypeViewMock(ILevelView level, ViewPrefabMock prefab) : base(level)
        {
            _prefab = prefab;
        }

        public T Create<T>(ContainerViewMock viewContainer) where T : IView
        {
            return (T)_prefab.CreateInContainer<T>(viewContainer);
        }
    }
}
