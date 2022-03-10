using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using System;
using UnityEngine.UI;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui
{
    public class ButtonUnityView : UnityView<ButtonView>
    {
        public bool IsShowing { get => GetButton().gameObject.activeSelf; set => GetButton().gameObject.SetActive(value); }
        public bool IsActive { get => GetButton().interactable; set => GetButton().interactable = value; }
        
        private Button _button;
        private Button GetButton()
        {
            if (_button == null)
                _button = GetComponent<Button>();
            return _button;
        }

        private void Click()
        {
            View.Click();
        }

        protected override void CreatedInner()
        {
            GetButton().onClick.AddListener(Click);
        }

        protected override void DisposeInner()
        {
            GetButton().onClick.RemoveListener(Click);
        }

        protected override ButtonView CreateView()
        {
            return new ButtonView(Level);
        }
    }
}
