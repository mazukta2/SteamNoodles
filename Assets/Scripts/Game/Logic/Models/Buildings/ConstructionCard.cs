using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using Tests.Assets.Scripts.Game.Logic.Views.Common;

namespace Assets.Scripts.Models.Buildings
{
    public class ConstructionCard : Disposable
    {
        public Point CellSize => _prototype.Size;
        public Requirements Requirements => _prototype.Requirements;
        public ISprite HandIcon => _prototype.HandIcon;
        public IVisual BuildingView => _prototype.BuildingView;
        public IConstructionSettings Protype => _prototype;

        public ConstructionCard(IConstructionSettings item)
        {
            _prototype = item;
        }

        protected override void DisposeInner()
        {
        }

        public Point[] GetOccupiedSpace(Point position)
        {
            return new ConstructionSettingsFunctions(_prototype).GetOccupiedSpace(position);
        }

        private IConstructionSettings _prototype { get; set; }
    }
}
