using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using Game.Tests.Mocks.Views.Common;
using System;
using Tests.Tests.Mocks.Views.Common;

namespace Game.Tests.Mocks.Views.Clashes
{
    public class ClashesView : TestView, IClashesView 
    {
        public IButtonView StartClash { get; } = new ButtonView();

        protected override void DisposeInner()
        {
        }
    }
}
