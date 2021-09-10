using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Logic.Prototypes.Levels;
using System;

namespace Tests.Mocks.Prototypes.Levels
{
    public class BasicBuildingPrototype : IBuildingPrototype
    {
        public Point Size => new Point(2, 1);

        public Requirements Requirements => new Requirements()
        {
            DownEdge = false
        };
    }
}
