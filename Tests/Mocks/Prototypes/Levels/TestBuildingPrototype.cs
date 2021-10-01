using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Tests.Mocks.Views.Common;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Common;
using Tests.Mocks.Prototypes.Levels;
using Tests.Tests.Mocks.Views.Common;

namespace Game.Tests.Mocks.Prototypes.Levels
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

        public TimeSpan WorkTime => new TimeSpan(0, 0, 2);
        public float WorkProgressPerHit => 10;
    }
}
