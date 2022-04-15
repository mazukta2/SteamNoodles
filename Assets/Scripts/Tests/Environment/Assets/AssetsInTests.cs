using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Environment.Engine.Assets;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Tests.Environment.Assets;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;
using System.Collections.Generic;

namespace Game.Tests.Controllers
{
    public class AssetsInTests : Disposable, IAssets
    {
        public ScreenAssetsMock Screens { get; } = new ScreenAssetsMock();
        IScreenAssets IAssets.Screens => Screens;

        private Dictionary<string, MockViewPrefab> _constructions = new Dictionary<string, MockViewPrefab>();

        protected override void DisposeInner()
        {
            Screens.Dispose();
            _constructions.Clear();
        }

        public IViewPrefab GetConstruction(string path)
        {
            if (!_constructions.ContainsKey(path))
                throw new System.Exception("Cant find resource : " + path);

            var prefab = _constructions[path];
            return prefab;
        }

        public IViewPrefab GetPrefab(string path)
        {
            throw new System.NotImplementedException();
        }

        public void AddPrefab(string path, MockViewPrefab prefab)
        {
            _constructions.Add(path, prefab);
        }

    }
}
