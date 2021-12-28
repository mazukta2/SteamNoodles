﻿using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Game.Assets.Scripts.Game.Logic.Settings.Constructions
{
    public interface IConstructionSettings
    {
        Point Size { get; }
        Requirements Requirements { get; }
        ISprite HandIcon { get; }
        IVisual BuildingView { get; }
        IReadOnlyCollection<IConstructionFeatureSettings> Features { get; }
        IReadOnlyDictionary<ConstructionTag, int> Tags { get; }
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
