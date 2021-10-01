using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.States;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Buildings
{
    public class Construction
    {
        private State _state;
        private uint _id;
        private GameState _entity => _state.Get<GameState>(_id);

        public ConstructionScheme Scheme => new ConstructionScheme(_entity.Prototype);
        public Point Position => _entity.Position;
        public TimeSpan WorkTime => _entity.Prototype.WorkTime;
        public float WorkProgressPerHit => _entity.Prototype.WorkProgressPerHit;
        public uint Id => _id;

        public Construction(State state, uint id)
        {
            _state = state;
            _id = id;
        }

        public Construction(State state, IConstructionPrototype prototype, Point position)
        {
            _state = state;
            (_id, _) = _state.Add(new GameState(prototype, position));
        }

        public bool IsProvide(IIngredientPrototype ingredient)
        {
            return Scheme.ProvidedIngridient == ingredient;
        }

        public Point[] GetOccupiedScace()
        {
            return Scheme.GetOccupiedSpace(Position);
        }

        public IVisual GetVisual()
        {
            return Scheme.BuildingView;
        }

        public struct GameState : IStateEntity
        {
            public IConstructionPrototype Prototype { get; }
            public Point Position { get; }

            public GameState(IConstructionPrototype prototype, Point position)
            {
                Prototype = prototype;
                Position = position;
            }
        }

    }
}
