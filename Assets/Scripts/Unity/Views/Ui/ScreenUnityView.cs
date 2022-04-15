using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Ui;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui
{
    public abstract class ScreenUnityView<TPresenter> : UnityView<TPresenter>
        where TPresenter : IPresenter
    {
    }

}
