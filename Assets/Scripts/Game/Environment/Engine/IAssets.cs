using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine.Assets;

namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface IAssets 
    {
        IScreenAssets Screens { get; }
        IViewPrefab GetConstruction(string path);
    }
}
