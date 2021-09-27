﻿using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Tests.Assets.Scripts.Game.Logic.Views.Common;
using Tests.Tests.Mocks.Views.Common;

namespace Tests.Mocks.Prototypes.Levels
{
    public class TestBuildingPrototype : IConstructionPrototype
    {
        public Point Size { get; set; } = new Point(2, 1);

        public Requirements Requirements { get; set; } = new Requirements()
        {
            DownEdge = false
        };

        public ISprite HandIcon { get; } = new ItsUnitySpriteWrapper();

        public IVisual BuildingView { get; } = new ItsUnitySpriteWrapper();
        public IIngredientPrototype ProvideIngredient { get; set; } = new TestIngredientPrototype();
    }
}