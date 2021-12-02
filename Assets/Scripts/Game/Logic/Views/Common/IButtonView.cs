using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public interface IButtonView
    {
        bool IsShowing { get; }
        void SetAction(Action action);
        void Click();
        void SetShowing(bool value);
    }
}
