using Game.Assets.Scripts.Game.Environment.Engine.Assets;
using Game.Assets.Scripts.Game.Logic.Common.Assets;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views.Ui;
using Game.Assets.Scripts.Tests.Mocks;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Environment.Assets
{
    public class ScreenAssetsInTests : Disposable, IScreenAssets
    {
        private Dictionary<string, Func<LevelInTests, ScreenView>> _list = new Dictionary<string, Func<LevelInTests, ScreenView>>();

        public void AddPrototype<TClass>(MockPrefab<TClass> prefab) where TClass : ScreenView, new()
        {
            _list.Add(typeof(TClass).Name, CreateScreen);

            TClass CreateScreen(LevelInTests level)
            {
                var s = new TClass();
                prefab.Spawn(level, s);
                level.Add(s);
                return s;
            }
        }

        protected override void DisposeInner()
        {
            _list.Clear();
        }


        public IScreenAsset<T> GetScreen<T>() where T : ScreenView
        {
            return new InnerResource<T>(this);
        }

        private class InnerResource<T> : IScreenAsset<T> where T : ScreenView
        {
            private ScreenAssetsInTests _assets;

            public InnerResource(ScreenAssetsInTests assets)
            {
                _assets = assets;
            }

            public T Create(ViewContainer container)
            {
                if (!_assets._list.ContainsKey(typeof(T).Name))
                    throw new System.Exception("Cant find resource : " + typeof(T).Name);
                return container.Create<T>(CreateItem);
            }

            private T CreateItem(LevelInTests level)
            {
                return (T)_assets._list[typeof(T).Name](level);
            }
        }

    }
}
