using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Unity.Views;

namespace Game.Assets.Scripts.Game.Logic.Common.Assets
{
    public interface IViewAsset<T> : IAsset where T : View
    {
    }
}
