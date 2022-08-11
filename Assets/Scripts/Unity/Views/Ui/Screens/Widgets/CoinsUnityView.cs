using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
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
    public class CoinsUnityView : UnityView<CustumerCoinsPresenter>, ICustumerCoinsView
    {
        [SerializeField] UnityText _text;

        public IText Text => _text;

        [SerializeField] AnimatorUnity _animator;
        public IAnimator Animator => _animator;
    }
}
