using Game.Assets.Scripts.Game.Environment.Engine.Assets;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;

namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface IAssets 
    {
        IScreenAssets Screens { get; }
        TestViewPrefab GetConstruction(string path);
    }
}
