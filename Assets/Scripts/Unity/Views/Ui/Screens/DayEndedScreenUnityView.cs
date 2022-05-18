using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class DayEndedScreenUnityView : ScreenUnityView<DayEndedScreenPresenter>, IDayEndedScreenView
    {
        [SerializeField] ButtonUnityView _nextDay;

        public IButton NextDayButton => _nextDay;
    }
}
