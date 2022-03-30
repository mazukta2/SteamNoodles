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
        public ScreenAssetsInTests Screens { get; } = new ScreenAssetsInTests();
        IScreenAssets IAssets.Screens => Screens;

        private Dictionary<string, TestViewPrefab> _constructions = new Dictionary<string, TestViewPrefab>();

        protected override void DisposeInner()
        {
            Screens.Dispose();
            _constructions.Clear();
        }

        public TestViewPrefab GetConstruction(string path)
        {
            if (!_constructions.ContainsKey(path))
                throw new System.Exception("Cant find resource : " + path);

            var prefab = _constructions[path];
            return prefab;
        }

        public void AddPrefab(string path, TestViewPrefab prefab)
        {
            _constructions.Add(path, prefab);
        }
    }
}
