using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using System;
#if UNITY_EDITOR
using UnityEngine.UI;
#endif

namespace Game.Assets.Scripts.Game.Unity.Views.Ui
{
    public class ButtonView : View<ButtonViewPresenter>
    {
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

        private void Click()
        {
            ViewPresenter.Click();
        }
#endif

        public ButtonViewPresenter ViewPresenter { get; private set; }
        public override ButtonViewPresenter GetViewPresenter() => ViewPresenter;

        protected override void CreatedInner()
        {
            ViewPresenter = new ButtonViewPresenter(Level);
#if UNITY
            GetButton().onClick.AddListener(Click);
#endif
        }

        protected override void DisposeInner()
        {
            ViewPresenter.Dispose();
#if UNITY
            GetButton().onClick.RemoveListener(Click);
#endif
        }

    }

    public class ButtonViewPresenter : ViewPresenter
    {
        public bool IsShowing { get; set; } = true;
        public bool IsActive { get; set; } = true;

        private Action _action;

        public ButtonViewPresenter(ILevel level) : base(level)
        {
        }

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

    }
}
