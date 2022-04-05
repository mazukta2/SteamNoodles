using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Ui;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui
{
    public abstract class ScreenUnityView<T> : UnityView<T> where T : class, IScreenView
    {
    }

}
