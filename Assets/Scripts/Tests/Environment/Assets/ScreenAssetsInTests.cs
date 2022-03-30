using Game.Assets.Scripts.Game.Environment.Engine.Assets;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Environment.Assets
{
    public class ScreenAssetsInTests : Disposable, IScreenAssets
    {
        private Dictionary<string, TestViewPrefab> _list = new Dictionary<string, TestViewPrefab>();

        public void AddPrototype<T>(TestViewPrefab prefab) where T : ScreenView
        {
            _list.Add(typeof(T).Name, prefab);
        }

        protected override void DisposeInner()
        {
            _list.Clear();
        }

        public TestViewPrefab GetScreen<T>() where T : ScreenView
        {
            if (!_list.ContainsKey(typeof(T).Name))
                throw new System.Exception("Cant find resource : " + typeof(T).Name);

            var prefab = _list[typeof(T).Name];
            return prefab;
        }
    }
}
