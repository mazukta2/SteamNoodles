using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Tests.Mocks.Views.Common;

namespace Game.Tests.Mocks.Views.Units
{
    public class UnitView : TestView, IUnitView
    {
        public FloatPoint Position { get; private set; }

        public void SetPosition(FloatPoint position)
        {
            Position = position;
        }

        protected override void DisposeInner()
        {
        }
    }
}
