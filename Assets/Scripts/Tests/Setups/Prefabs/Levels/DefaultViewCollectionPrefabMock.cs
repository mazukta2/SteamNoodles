using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;

namespace Game.Assets.Scripts.Tests.Setups.Prefabs.Levels
{
    public class DefaultViewCollectionPrefabMock : ViewCollectionPrefabMock
    {
        private Action<IViewsCollection> _action;

        public DefaultViewCollectionPrefabMock(Action<IViewsCollection> action)
        {
            _action = action;
        }

        public override void Fill(IViewsCollection collection)
        {
            _action(collection);
        }
    }
}
