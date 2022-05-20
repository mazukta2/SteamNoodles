using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Screens.Widgets
{
    public class BuildingTooltipView : UnityView<BuildingTooltipPresenter>, IBuildingToolitpView
    {
        [SerializeField] UnityAnimator _animator;
        [SerializeField] HandConstructionTooltipUnityView _tooltip;

        public IHandConstructionTooltipView Tooltip => _tooltip;
        public IAnimator Animator => _animator;
    }
}
