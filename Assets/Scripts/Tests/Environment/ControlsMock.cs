using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Views;
using System;

namespace Game.Assets.Scripts.Tests.Environment
{
    public class ControlsMock : IControls
    {
        public event Action OnLevelClick = delegate { };
        public event Action<FloatPoint> OnLevelPointerMoved = delegate { };

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
