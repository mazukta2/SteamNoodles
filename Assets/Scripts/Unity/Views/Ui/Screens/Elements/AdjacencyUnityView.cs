using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Elements;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class AdjacencyUnityView : ScreenUnityView<MainScreenPresenter>, IAdjacencyTextView
    {
        [SerializeField] UnityWorldText _text;
        public IWorldText Text => _text;
    }
}
