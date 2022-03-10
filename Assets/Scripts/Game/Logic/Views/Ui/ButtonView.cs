using Game.Assets.Scripts.Game.Environment.Engine;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public class ButtonView : View
    {
        public bool IsShowing { get; set; } = true;
        public bool IsActive { get; set; } = true;

        private Action _action;

        public ButtonView(ILevel level) : base(level)
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
