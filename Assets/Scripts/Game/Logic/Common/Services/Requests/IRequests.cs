using Game.Assets.Scripts.Game.Logic.Presenters.Requests;

namespace Game.Assets.Scripts.Game.Logic.Common.Services.Requests
{
    public interface IRequests
    {
        public static IRequests Default { get; set; }

        public T Send<T>(T request) where T : IRequest;
        RequestLink<T> Get<T>() where T : IRequest;
    }
}
