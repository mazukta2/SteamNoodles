using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;

namespace Game.Assets.Scripts.Tests.Setups.Prefabs.Levels
{
    public class DefaultViewCollectionPrefabMock : ViewCollectionPrefabMock
    {
        private Action<IViews> _action;

        public DefaultViewCollectionPrefabMock(Action<IViews> action)
        {
            _action = action;
        }

        public override void Fill(IViews collection)
        {
            _action(collection);
        }
    }
}
