using System;
#if UNITY_EDITOR
using UnityEngine.UI;
#endif

namespace Game.Assets.Scripts.Game.Unity.Views.Ui
{
    public class ButtonView : View
    {
        private Action _action;

#if UNITY
        public bool IsShowing { get => GetButton().gameObject.activeSelf; set => GetButton().gameObject.SetActive(value); }
        public bool IsActive { get => GetButton().interactable; set => GetButton().interactable = value; }
        
        private Button _button;
        private Button GetButton()
        {
            if (_button == null)
                _button = GetComponent<Button>();
            return _button;
        }

#else
        public bool IsShowing { get; set; } = true;
        public bool IsActive { get; set; } = true;
#endif

        public void SetAction(Action action)
        {
            _action = action;
        }

        public void Click()
        {
            if (!IsShowing)
                throw new Exception("Button is not showing");

            if (_action == null)
                throw new Exception("There is no action available");

            if (!IsActive)
                return;

            _action();
        }

#if UNITY
        protected override void CreatedInner()
        {
            GetButton().onClick.AddListener(Click);
        }

        protected override void DisposeInner()
        {
            GetButton().onClick.RemoveListener(Click);
        }
#endif


    }
}
