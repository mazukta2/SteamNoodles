using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace GameUnity.Assets.Scripts.Unity.Common
{
    public class UnityButton : IButtonView
    {
        public event Action OnDispose = delegate { };
        public bool IsDisposed { get; private set; }
        public bool IsShowing => _button.gameObject.activeSelf;
        private Button _button;
        private Action _action;

        public UnityButton(Button button)
        {
            _button = button;
            _button.onClick.AddListener(Click);
        }

        public void Dispose()
        {
            _button.onClick.RemoveListener(Click);
            IsDisposed = true;
            OnDispose();
        }

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
    }
}
