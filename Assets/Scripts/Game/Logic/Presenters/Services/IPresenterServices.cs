using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.DataObjects;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Services
{
    public interface IPresenterServices
    {
        public static IPresenterServices Default { get; set; }
        public T Get<T>() where T : IService;
    }
}
