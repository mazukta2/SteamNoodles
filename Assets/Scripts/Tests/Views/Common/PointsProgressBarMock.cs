using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Views.Common
{
    public class PointsProgressBarMock : IPointsProgressBar
    {
        public float MainValue { get; set; }
        public float AddedValue { get; set; }
        public float RemovedValue { get; set; }
    }
}
