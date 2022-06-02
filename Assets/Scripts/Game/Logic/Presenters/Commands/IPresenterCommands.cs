namespace Game.Assets.Scripts.Game.Logic.Presenters.Commands
{
    public interface IPresenterCommands
    {
        public static IPresenterCommands Default { get; set; }

        public void Execute(IPresenterCommand command);
    }
}
