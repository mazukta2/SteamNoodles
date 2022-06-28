namespace Game.Assets.Scripts.Game.Logic.Libs.Stateless
{
    public partial class StateMachine<TState, TTrigger>
    {
        internal class StateReference
        {
            public TState State { get; set; }
        }
    }
}
