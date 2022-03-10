using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public class PrototypeView : View
    {
        private ViewPrefab _prefab;

        public PrototypeView(ILevel level, ViewPrefab prefab) : base(level)
        {
            _prefab = prefab;
        }
        
        public T Create<T>(ContainerView viewContainer) where T : View
        {
            return (T)_prefab.Create<T>(viewContainer);
        }
    }
}
