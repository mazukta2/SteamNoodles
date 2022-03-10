using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Views.Ui;

namespace Game.Assets.Scripts.Game.Environment.Engine.Assets
{
    public interface IScreenAssets
    {
        ViewPrefab GetScreen<T>() where T : ScreenView;
    }
}
