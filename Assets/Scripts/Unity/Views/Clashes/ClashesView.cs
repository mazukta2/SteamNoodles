using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameUnity.Assets.Scripts.Unity.Views.Units
{
    public class ClashesView : GameMonoBehaviour, IClashesView
    {
        [SerializeField] Button _nextButton;

        private Action _onStartClash;

        protected void OnEnable()
        {
            _nextButton.onClick.AddListener(OnNextClick);
        }

        protected void OnDisable()
        {
            _nextButton.onClick.RemoveListener(OnNextClick);
        }

        public void SetStartClashAction(Action onStartClash)
        {
            _onStartClash = onStartClash;
        }

        public void ShowButton(bool v)
        {
            _nextButton.gameObject.SetActive(v);
        }

        private void OnNextClick()
        {
            _onStartClash();
        }

    }
}