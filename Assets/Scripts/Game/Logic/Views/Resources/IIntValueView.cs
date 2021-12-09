using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Game
{
    public interface IIntValueView : IView
    {
        int GetValue();
        void SetValue(int value);
    }
}
