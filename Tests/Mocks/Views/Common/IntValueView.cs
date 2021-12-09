using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Tests.Mocks.Views.Common;

namespace Game.Tests.Mocks.Views.Common
{
    public class IntValueView : TestView, IIntValueView
    {
        public int Value { get; set; }

        public int GetValue()
        {
            return Value;
        }

        public void SetValue(int value)
        {
            Value = value;
        }

        protected override void DisposeInner()
        {
        }
    }
}
