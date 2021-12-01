using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;
using Tests.Tests.Mocks.Views.Common;

namespace Game.Tests.Mocks.Views.Levels
{
    public class BasicHandView : TestView, IHandView
    {
        public IHandConstructionView CreateConstruction()
        {
            return new BasicHandConstructionView();
        }
        protected override void DisposeInner()
        {
        }
    }
}
