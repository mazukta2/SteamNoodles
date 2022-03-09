using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine.Assets;
using Game.Assets.Scripts.Game.Logic.Common.Assets;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views.Ui;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Environment.Assets
{
    public class ScreenAssetsInTests : Disposable, IScreenAssets
    {
        private Dictionary<string, IPrefab> _list = new Dictionary<string, IPrefab>();

        public void AddPrototype<T>(IPrefab prefab) where T : ScreenViewPresenter
        {
            _list.Add(typeof(T).Name, prefab);
        }

        protected override void DisposeInner()
        {
            _list.Clear();
        }

        public ViewPrefab<T> GetScreen<T>() where T : ScreenViewPresenter
        {
            if (!_list.ContainsKey(typeof(T).Name))
                throw new System.Exception("Cant find resource : " + typeof(T).Name);

            var prefab = _list[typeof(T).Name];
            return (ViewPrefab<T>)prefab;
        }
    }
}
