using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Common;

namespace Assets.Scripts.Logic.Prototypes.Levels
{
    public interface IConstructionSettings
    {
        Point Size { get; }
        Requirements Requirements { get;}
        ISprite HandIcon { get; }
        IVisual BuildingView { get; }
        float WorkTime { get; }
        float WorkProgressPerHit { get; }
    }

    [Serializable]
    public struct Requirements
    {
        public bool DownEdge;
    }

}
