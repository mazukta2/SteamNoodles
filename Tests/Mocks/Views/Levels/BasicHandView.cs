using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Tests.Tests.Mocks.Views.Levels
{
    public class BasicHandView : IHandView
    {
        public IHandConstructionView CreateConstrcution()
        {
            return new BasicHandConstructionView();
        }
    }
}
