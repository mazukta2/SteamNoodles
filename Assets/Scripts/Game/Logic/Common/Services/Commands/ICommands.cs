namespace Game.Assets.Scripts.Game.Logic.Common.Services.Commands
{
    public interface ICommands
    {
        public static ICommands Default { get; set; }

        public void Execute<T>(T command) where T : ICommand;
    }
}
