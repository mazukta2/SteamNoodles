using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine.Assets;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Environment.Assets
{
    public class ScreenAssetsInTests : Disposable, IScreenAssets
    {
        private Dictionary<string, ViewPrefab> _list = new Dictionary<string, ViewPrefab>();

        public void AddPrototype<T>(ViewPrefab prefab) where T : ScreenView
        {
            _list.Add(typeof(T).Name, prefab);
        }

        protected override void DisposeInner()
        {
            _list.Clear();
        }

        public ViewPrefab GetScreen<T>() where T : ScreenView
        {
            if (!_list.ContainsKey(typeof(T).Name))
                throw new System.Exception("Cant find resource : " + typeof(T).Name);

            var prefab = _list[typeof(T).Name];
            return prefab;
        }
    }
}
