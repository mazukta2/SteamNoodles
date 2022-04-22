using Game.Assets.Scripts.Game.Environment.Creation;

namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface IAssets 
    {
        IViewPrefab GetPrefab(string path);
        static IAssets Default { get; set; }
    }
}
