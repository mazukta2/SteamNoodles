using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Common;

namespace Assets.Scripts.Logic.Prototypes.Levels
{
    public interface IConstructionPrototype
    {
        Point Size { get; }
        Requirements Requirements { get;}
        ISprite HandIcon { get; }
        IVisual BuildingView { get; }
        IIngredientPrototype ProvideIngredient { get; }
        TimeSpan WorkTime { get; }
        float WorkProgressPerHit { get; }
    }

    [Serializable]
    public struct Requirements
    {
        public bool DownEdge;
    }

}
