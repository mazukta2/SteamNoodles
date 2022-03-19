using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Constructions
{
    public class Construction : Disposable
    {
        public IntPoint Position { get; private set; }

        private PlacementField _field;

        public ConstructionDefinition Definition { get; private set; }

        public Construction(PlacementField field, ConstructionDefinition definition, IntPoint position)
        {
            Definition = definition ?? throw new ArgumentNullException(nameof(definition));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            Position = position;
        }

        public FloatPoint GetLocalPosition()
        {
            return _field.GetLocalPosition(Position);
        }

        public IReadOnlyCollection<IntPoint> GetOccupiedScace()
        {
            return Definition.GetOccupiedSpace(Position);
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
