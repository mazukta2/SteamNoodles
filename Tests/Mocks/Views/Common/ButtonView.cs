using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Tests.Mocks.Views.Common;

namespace Game.Tests.Mocks.Views.Common
{
    public class ButtonView : TestView, IButtonView
    {
        public bool IsShowing { get; private set; }
        private Action _action;


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
            IsShowing = value;
        }

        protected override void DisposeInner()
        {
        }
    }
}
