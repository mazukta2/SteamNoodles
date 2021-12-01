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
    public class TestBuildingPrototype : IConstructionSettings
    {
        public Point Size { get; set; } = new Point(2, 1);

        public Requirements Requirements { get; set; } = new Requirements()
        {
            DownEdge = false
        };

        public ISprite HandIcon { get; } = new ItsUnitySpriteWrapper();

        public IVisual BuildingView { get; } = new ItsUnitySpriteWrapper();

        public float WorkTime => 2f;
        public float WorkProgressPerHit => 10;
    }
}
