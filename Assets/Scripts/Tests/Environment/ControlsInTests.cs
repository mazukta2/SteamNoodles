using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Environment.Engine.Controls;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Environment
{
    public class ControlsInTests : IControls
    {
        public event Action OnLevelClick = delegate { };
        public event Action<FloatPoint> OnLevelPointerMoved = delegate { };
        public GameKeysManager Keys { get; } = new GameKeysManager();

        public void Click()
        {
            OnLevelClick();
        }

        public void MovePointer(FloatPoint floatPoint)
        {
            OnLevelPointerMoved(floatPoint);
        }

    }
}
