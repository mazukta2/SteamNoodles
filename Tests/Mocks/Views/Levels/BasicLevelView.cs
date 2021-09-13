using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Tests.Tests.Mocks.Views.Levels
{
    public class BasicLevelView : ILevelView
    {
        public IHandView CreateHand()
        {
            return new BasicHandView();
        }
    }
}
