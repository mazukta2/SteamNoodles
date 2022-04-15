using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine.Assets;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Environment.Assets
{
    public class ScreenAssetsMock : Disposable, IScreenAssets
    {
        private Dictionary<string, MockViewPrefab> _list = new Dictionary<string, MockViewPrefab>();

        public void AddPrototype<T>(MockViewPrefab prefab) where T : IScreenView
        {
            _list.Add(typeof(T).Name, prefab);
        }

        protected override void DisposeInner()
        {
            _list.Clear();
        }

        public IViewPrefab GetScreen<T>() where T : IScreenView
        {
            var name = typeof(T).Name;
            name = name.Remove(0, 1);

            if (!_list.ContainsKey(name))
                throw new System.Exception("Cant find resource : " + typeof(T).Name);

            var prefab = _list[name];
            return prefab;
        }
    }
}
