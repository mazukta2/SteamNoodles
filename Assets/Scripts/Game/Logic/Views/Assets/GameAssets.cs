using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Assets
{
    public class GameAssets : IGameAssets
    {
        private IAssets _assets;
        public GameAssets(IAssets assets) => (_assets) = (assets);
        public IViewPrefab GetPrefab(string path) => _assets.GetPrefab(path);
    }
}
