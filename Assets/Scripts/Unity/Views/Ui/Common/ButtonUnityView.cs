using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui
{
    public class ButtonUnityView : MonoBehaviour, IButton
    {
        public bool IsShowing { get => GetButton().gameObject.activeSelf; set => GetButton().gameObject.SetActive(value); }
        public bool IsActive { get => GetButton().interactable; set => GetButton().interactable = value; }
        
        private Button _button;
        private Action _action;

        private Button GetButton()
        {
            if (_button == null)
                _button = GetComponent<Button>();
            return _button;
        }

        public void Click()
        {
            _action();
        }

        protected void OnEnable()
        {
            GetButton().onClick.AddListener(Click);
        }

        protected void OnDisable()
        {
            GetButton().onClick.RemoveListener(Click);
        }

        public void SetAction(Action action)
        {
            _action = action;
        }

    }
}
