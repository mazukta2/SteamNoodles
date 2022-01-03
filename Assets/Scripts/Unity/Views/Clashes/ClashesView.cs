using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using GameUnity.Assets.Scripts.Unity.Views.Common;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameUnity.Assets.Scripts.Unity.Views.Units
{
    public class ClashesView : ViewMonoBehaviour, IClashesView
    {
        [SerializeField] ButtonView _nextButton;

        public IButtonView StartClash => _nextButton;
    }
}