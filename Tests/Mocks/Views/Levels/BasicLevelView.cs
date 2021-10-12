﻿using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;
using Tests.Tests.Mocks.Views.Common;
using Tests.Tests.Mocks.Views.Levels;

namespace Game.Tests.Mocks.Views.Levels
{
    public class BasicLevelView : TestView, ILevelView
    {
        public IHandView CreateHand()
        {
            return new BasicHandView();
        }

        public IPlacementView CreatePlacement()
        {
            return new BasicPlacementView();
        }

        public ICurrentOrderView CreateCurrentOrder()
        {
            return new BasicCurrentOrderView();
        }
    }
}
