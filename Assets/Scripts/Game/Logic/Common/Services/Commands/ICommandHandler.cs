namespace Game.Assets.Scripts.Game.Logic.Common.Services.Commands
{
    public interface ICommandHandler<T> : IBaseCommandHandler where T : ICommand
    {
        void Handle(T command);
    }

    public interface IBaseCommandHandler
    {

    }
}
