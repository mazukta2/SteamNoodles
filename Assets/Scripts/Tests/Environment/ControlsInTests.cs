using Game.Assets.Scripts.Game.Environment.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Environment
{
    public class ControlsInTests : IControls
    {
        public event Action OnLevelClick = delegate { };

        public void Click()
        {
            OnLevelClick();
        }
    }
}
