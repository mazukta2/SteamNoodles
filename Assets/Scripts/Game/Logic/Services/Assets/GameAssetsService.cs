using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Services;

namespace Game.Assets.Scripts.Game.Logic.Services.Assets
{
    public class GameAssetsService : IService
    {
        private IAssets _assets;
        public GameAssetsService(IAssets assets) => _assets = assets;
        public IViewPrefab GetPrefab(string path) => _assets.GetPrefab(path);
    }
}
