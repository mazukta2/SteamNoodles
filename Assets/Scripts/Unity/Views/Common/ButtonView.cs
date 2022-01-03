using Assets.Scripts.Core;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameUnity.Assets.Scripts.Unity.Views.Common
{
    public class ButtonView : ViewMonoBehaviour, IButtonView
    {
        [SerializeField] Button _button;
        private Action _action;

        public bool IsShowing => _button.gameObject.activeSelf;

        public void Click()
        {
            _action();
        }

        public void SetAction(Action action)
        {
            _action = action;
        }

        public void SetShowing(bool value)
        {
            _button.gameObject.SetActive(value);
        }


        protected void OnEnable()
        {
            _button.onClick.AddListener(Click);
        }

        protected void OnDisable()
        {
            _button.onClick.RemoveListener(Click);
        }
    }
}
