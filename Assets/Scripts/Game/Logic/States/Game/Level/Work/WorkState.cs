namespace Game.Assets.Scripts.Game.Logic.States.Game.Level
{
    public struct WorkState : IStateEntity
    {
        private State _state;
        public uint Id { get; }

        public WorkState(State state, uint id)
        {
            _state = state;
            Id = id;
        }
    }
}
