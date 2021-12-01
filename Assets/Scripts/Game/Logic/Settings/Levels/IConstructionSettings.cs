using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
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

    public class ConstructionSettingsFunctions
    {
        private IConstructionSettings _settings;

        public ConstructionSettingsFunctions(IConstructionSettings settings)
        {
            _settings = settings;
        }

        public Point[] GetOccupiedSpace(Point position)
        {
            var result = new List<Point>();
            for (int x = 0; x < _settings.Size.X; x++)
            {
                for (int y = 0; y < _settings.Size.Y; y++)
                {
                    result.Add(position + new Point(x, y));
                }
            }
            return result.ToArray();
        }
    }
}
