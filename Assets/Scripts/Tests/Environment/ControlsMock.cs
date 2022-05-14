using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;

namespace Game.Assets.Scripts.Tests.Environment
{
    public class ControlsMock : IControls
    {
        public FloatPoint3D PointerLevelPosition { get; private set; }

        public event Action OnLevelClick = delegate { };
        public event Action<FloatPoint3D> OnLevelPointerMoved = delegate { };

        public void Click()
        {
            OnLevelClick();
        }

        public void MovePointer(FloatPoint3D floatPoint)
        {
            PointerLevelPosition = floatPoint;
            OnLevelPointerMoved(floatPoint);
        }

        public void ShakeCamera()
        {
        }
    }
}
