using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;

namespace Game.Assets.Scripts.Game.Logic.States.Game.Level
{
    public struct ConstructionState : IStateEntity
    {
        private State _state;
        public uint Id { get; }
        public IConstructionPrototype Prototype { get; }
        public Point Position { get; }

        public ConstructionState(State state, uint id, IConstructionPrototype prototype, Point position)
        {
            _state = state;
            Id = id;
            Prototype = prototype;
            Position = position;
        }
    }
}
