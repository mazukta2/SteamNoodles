using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class GameMenuScreenUnityView : ScreenUnityView<GameMenuScreenPresenter>, IGameMenuScreenView
    {
        [SerializeField] ButtonUnityView _close;
        [SerializeField] ButtonUnityView _exit;

        public IButton Close => _close;
        public IButton ExitGame => _exit;
    }
}
