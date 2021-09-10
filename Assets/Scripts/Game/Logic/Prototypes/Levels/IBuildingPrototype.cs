using Assets.Scripts.Game.Logic.Common.Math;

namespace Assets.Scripts.Logic.Prototypes.Levels
{
    public interface IBuildingPrototype
    {
        Point Size { get; }

        Requirements Requirements { get;}
    }

    public struct Requirements
    {
        public bool DownEdge;
    }

}
