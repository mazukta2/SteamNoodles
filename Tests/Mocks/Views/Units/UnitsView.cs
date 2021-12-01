using Game.Assets.Scripts.Game.Logic.Views.Units;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Tests.Mocks.Views.Common;

namespace Game.Tests.Mocks.Views.Units
{
    public class UnitsView : TestView, IUnitsView
    {
        public IUnitView CreateUnit()
        {
            return new UnitView();
        }
        protected override void DisposeInner()
        {
        }
    }
}
