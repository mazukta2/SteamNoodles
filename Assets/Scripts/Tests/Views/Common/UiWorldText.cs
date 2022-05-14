using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Views.Common
{
    public class UiWorldText : IWorldText
    {
        public string Value { get; set; } = "";
        public FloatPoint3D Position { get; set; }
    }
}
