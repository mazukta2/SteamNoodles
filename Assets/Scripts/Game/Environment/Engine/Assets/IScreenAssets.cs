using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;

namespace Game.Assets.Scripts.Game.Environment.Engine.Assets
{
    public interface IScreenAssets
    {
        TestViewPrefab GetScreen<T>() where T : ScreenView;
    }
}
