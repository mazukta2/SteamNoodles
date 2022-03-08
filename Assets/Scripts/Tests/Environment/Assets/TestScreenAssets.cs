using Game.Assets.Scripts.Game.Environment.Engine.Assets;
using Game.Assets.Scripts.Game.Logic.Common.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views.Ui;
using Game.Assets.Scripts.Tests.Mocks;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Environment.Assets
{
    public class TestScreenAssets : IScreenAssets
    {
        private Dictionary<string, Func<ScreenView>> _list = new Dictionary<string, Func<ScreenView>>();
        private List<IDisposable> _prefabDisposables = new List<IDisposable>();

        public void AddPrototype<TClass>(MockPrefab<TClass> prefab) where TClass : ScreenView, new()
        {
            _list.Add(typeof(TClass).Name, CreateScreen);

            TClass CreateScreen()
            {
                var s = new TClass();
                prefab.Mock(_prefabDisposables, s);
                return s;
            }
        }

        public void Dispose()
        {
            foreach (var item in _prefabDisposables)
                item.Dispose();
            _prefabDisposables.Clear();
            _list.Clear();
        }

        public IScreenAsset<T> GetScreen<T>() where T : ScreenView
        {
            return new InnerResource<T>(this);
        }

        private class InnerResource<T> : IScreenAsset<T> where T : ScreenView
        {
            private TestScreenAssets _assets;

            public InnerResource(TestScreenAssets assets)
            {
                _assets = assets;
            }

            public T Create(ViewContainer container)
            {
                if (!_assets._list.ContainsKey(typeof(T).Name))
                    throw new System.Exception("Cant find resource : " + typeof(T).Name);
#if !UNITY
                return container.Create<T>(CreateItem);
#else
                throw new Exception("unavailable in unity");
#endif
            }

            private T CreateItem()
            {
                return (T)_assets._list[typeof(T).Name]();
            }
        }

    }
}
