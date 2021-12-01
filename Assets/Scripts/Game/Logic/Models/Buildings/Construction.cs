using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.States;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Buildings
{
    public class Construction
    {
        private GameState _state;

        public Construction(IConstructionSettings prototype, Point position)
        {
            _state = new GameState();
            _state.Prototype = prototype;
            _state.Position = position;
        }

        public ConstructionScheme Scheme => new ConstructionScheme(_state.Prototype);

        public Point Position => _state.Position;
        public float WorkTime => _state.Prototype.WorkTime;
        public float WorkProgressPerHit => _state.Prototype.WorkProgressPerHit;

        public Point[] GetOccupiedScace()
        {
            return Scheme.GetOccupiedSpace(Position);
        }

        public IVisual GetVisual()
        {
            return Scheme.BuildingView;
        }

        private class GameState : IStateEntity
        {
            public IConstructionSettings Prototype { get; set; }
            public Point Position { get; set; }
        }

    }
}
