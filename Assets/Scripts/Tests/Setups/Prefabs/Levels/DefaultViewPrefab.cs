using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;

namespace Game.Assets.Scripts.Tests.Setups.Prefabs.Levels
{
    public class DefaultViewPrefab : ViewCollectionPrefabMock
    {
        private Action<IViewsCollection> _action;

        public DefaultViewPrefab(Action<IViewsCollection> action)
        {
            _action = action;
        }

        public override void Fill(IViewsCollection collection)
        {
            _action(collection);
        }
    }
}
