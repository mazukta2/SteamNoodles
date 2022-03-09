using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views.Ui;

namespace Game.Assets.Scripts.Game.Logic.Common.Assets
{
    public interface IScreenAsset<T> : IViewAsset<T> where T : ViewPresenter
    {
        T Create(ContainerViewPresenter container);
    }
}
