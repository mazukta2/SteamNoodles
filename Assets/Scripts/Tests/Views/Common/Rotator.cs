using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Game.Assets.Scripts.Tests.Views.Common
{
    public class Rotator : IRotator
    {
        public GameQuaternion Rotation { get; set; }
    }
}
