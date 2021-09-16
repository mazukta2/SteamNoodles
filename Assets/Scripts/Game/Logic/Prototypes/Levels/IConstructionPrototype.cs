using Assets.Scripts.Game.Logic.Common.Math;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Common;

namespace Assets.Scripts.Logic.Prototypes.Levels
{
    public interface IConstructionPrototype
    {
        Point Size { get; }

        Requirements Requirements { get;}
        ISprite HandIcon { get; }
    }

    [Serializable]
    public struct Requirements
    {
        public bool DownEdge;
    }

}
