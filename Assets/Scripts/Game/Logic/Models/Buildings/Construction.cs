using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Buildings
{
    public class Construction : Disposable
    {
        public Point Position { get; private set; }
        public float WorkTime => _prototype.WorkTime;
        public float WorkProgressPerHit => _prototype.WorkProgressPerHit;

        private IConstructionSettings _prototype { get; set; }

        public Construction(IConstructionSettings prototype, Point position)
        {
            _prototype = prototype;
            Position = position;
        }

        protected override void DisposeInner()
        {
        }

        public Point[] GetOccupiedScace()
        {
            return new ConstructionSettingsFunctions(_prototype).GetOccupiedSpace(Position);
        }

        public IVisual GetVisual()
        {
            return _prototype.BuildingView;
        }

    }
}
