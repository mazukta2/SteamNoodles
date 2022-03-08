using System;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Constructions
{
    public interface IConstructionDefinition
    {
        //Point Size { get; }
        //Requirements Requirements { get; }
        //ISprite HandIcon { get; }
        //IVisual BuildingView { get; }
        //IReadOnlyCollection<IConstructionFeatureSettings> Features { get; }
        //IReadOnlyDictionary<ConstructionTag, int> Tags { get; }
    }

    [Serializable]
    public struct Requirements
    {
        public bool DownEdge;
    }

    public class ConstructionSettingsFunctions
    {
        private IConstructionDefinition _settings;

        public ConstructionSettingsFunctions(IConstructionDefinition settings)
        {
            _settings = settings;
        }

        //public Point[] GetOccupiedSpace(Point position)
        //{
        //    var result = new List<Point>();
        //    for (int x = 0; x < _settings.Size.X; x++)
        //    {
        //        for (int y = 0; y < _settings.Size.Y; y++)
        //        {
        //            result.Add(position + new Point(x, y));
        //        }
        //    }
        //    return result.ToArray();
        //}
    }
}
