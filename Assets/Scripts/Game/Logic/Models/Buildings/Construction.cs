using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Buildings
{
    public class Construction : Disposable
    {
        public Point Position { get; private set; }

        private IConstructionSettings _settings { get; set; }

        public Construction(IConstructionSettings settings, Point position)
        {
            _settings = settings;
            Position = position;
        }

        protected override void DisposeInner()
        {
        }

        public Point[] GetOccupiedScace()
        {
            return new ConstructionSettingsFunctions(_settings).GetOccupiedSpace(Position);
        }

        public IVisual GetVisual()
        {
            return _settings.BuildingView;
        }

        public IConstructionFeatureSettings[] GetFeatures()
        {
            return _settings.Features;
        }

        public IConstructionSettings GetSettings()
        {
            return _settings;
        }
    }
}
