using Assets.Scripts.Game.Logic.Common.Math;
using Tests.Assets.Scripts.Game.Logic.Views.Common;

namespace Assets.Scripts.Logic.Prototypes.Levels
{
    public interface IBuildingPrototype
    {
        Point Size { get; }

        Requirements Requirements { get;}
        ISprite HandIcon { get; }
    }

    public struct Requirements
    {
        public bool DownEdge;
    }

}
