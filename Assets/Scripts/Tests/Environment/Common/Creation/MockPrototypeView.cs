using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Tests.Environment.Common.Creation
{
    public class MockPrototypeView : View, IViewPrefab
    {
        private TestViewPrefab _prefab;

        public MockPrototypeView(ILevel level, TestViewPrefab prefab) : base(level)
        {
            _prefab = prefab;
        }

        public T Create<T>(MockContainerView viewContainer) where T : View
        {
            return (T)_prefab.CreateInContainer<T>(viewContainer);
        }
    }
}
