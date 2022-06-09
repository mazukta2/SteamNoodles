namespace Game.Assets.Scripts.Game.Logic.Common.Services.Requests
{
    public interface IRequestHandler<T> : IBaseRequestHandler where T : IRequest
    {
        void Handle(T request);
    }

    public interface IBaseRequestHandler
    {

    }
}
