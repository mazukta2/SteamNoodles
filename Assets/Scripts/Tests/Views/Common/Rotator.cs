using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Views.Common
{
    public class Rotator : IRotator
    {
        private FloatPoint3D Direction { get; set; }

        public FloatPoint3D GetDirection()
        {
            return Direction;
        }

        public void LookAtDirection(FloatPoint3D direction)
        {
            Direction = direction;
        }

        public FloatPoint3D MoveTowards(FloatPoint3D target, float speed)
        {
            return target;
        }
    }
}
