using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Environment
{
    public class AssetsMock : IAssets
    {
        private Dictionary<string, MockViewPrefab> _prefabs = new Dictionary<string, MockViewPrefab>();

        public IViewPrefab GetPrefab(string path)
        {
            if (!_prefabs.ContainsKey(path))
                throw new System.Exception("Cant find resource : " + path);

            var prefab = _prefabs[path];
            return prefab;
        }

        public void AddPrefab(string path, MockViewPrefab prefab)
        {
            _prefabs.Add(path, prefab);
        }

        public void ClearPrefabs()
        {
            _prefabs.Clear();
        }

    }
}
