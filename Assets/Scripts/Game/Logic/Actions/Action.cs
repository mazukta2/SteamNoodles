using Game.Assets.Scripts.Game.Logic.States;

namespace Game.Assets.Scripts.Game.Logic.Actions
{
    public abstract class GameAction
    {
        protected State State { get; private set; }

        public void SetContext(State state)
        {
            State = state;
        }

        public abstract void Execute();
    }
}
