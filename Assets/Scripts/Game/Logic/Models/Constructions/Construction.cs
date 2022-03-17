﻿using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Constructions
{
    public class Construction : Disposable
    {
        public IntPoint Position { get; private set; }
        private ConstructionDefinition _definition;

        public Construction(ConstructionDefinition definition, IntPoint position)
        {
            _definition = definition;
            Position = position;
        }

        public IReadOnlyCollection<IntPoint> GetOccupiedScace()
        {
            return _definition.GetOccupiedSpace(Position);
        }

        //public IVisual GetVisual()
        //{
        //    return _settings.BuildingView;
        //}

        //public IReadOnlyCollection<IConstructionFeatureSettings> GetFeatures()
        //{
        //    return _settings.Features;
        //}

        //public IConstructionSettings GetSettings()
        //{
        //    return _settings;
        //}

        //public int GetTagsCount(ConstructionTag tag)
        //{
        //    if (_settings.Tags.ContainsKey(tag))
        //        return _settings.Tags[tag];

        //    return 0;
        //}
    }
}