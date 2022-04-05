using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public interface IButton
    {
        bool IsShowing { get; set; }
        bool IsActive { get; set; }
        void SetAction(Action action);
        void Click();
    }
}
