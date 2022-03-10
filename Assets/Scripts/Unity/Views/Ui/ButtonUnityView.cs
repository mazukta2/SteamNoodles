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
            ViewPresenter.Click();
        }

        public ButtonView ViewPresenter { get; private set; }
        public override ButtonView GetView() => ViewPresenter;

        protected override void CreatedInner()
        {
            ViewPresenter = new ButtonView(Level);
            GetButton().onClick.AddListener(Click);
        }

        protected override void DisposeInner()
        {
            ViewPresenter.Dispose();
            GetButton().onClick.RemoveListener(Click);
        }

    }
}
