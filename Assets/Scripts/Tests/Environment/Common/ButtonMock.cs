using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Tests.Environment.Common
{
    public class ButtonMock : IButton
    {
        public bool IsShowing { get; set; } = true;
        public bool IsActive { get; set; } = true;

        private Action _action;

        public void SetAction(Action action)
        {
            _action = action;
        }

        public void Click()
        {
            if (!IsShowing)
                throw new Exception("Button is not showing");

            if (_action == null)
                throw new Exception("There is no action setted");

            if (!IsActive)
                return;

            _action();
        }

    }
}
