using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using TMPro;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class MainScreenUnityView : ScreenUnityView<MainScreenView>
    {
        [SerializeField] HandUnityView _hand;
        [SerializeField] TextMeshProUGUI _points;

        protected override MainScreenView CreateView()
        {
            return new MainScreenView(Level, _hand.View, new UnityText(_points));
        }

    }
}
