using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Ui;

namespace Game.Assets.Scripts.Game.Environment.Engine.Assets
{
    public interface IScreenAssets
    {
        IViewPrefab GetScreen<T>() where T : IScreenView;
    }
}
