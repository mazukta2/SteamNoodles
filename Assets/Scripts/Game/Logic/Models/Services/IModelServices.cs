using Game.Assets.Scripts.Game.Logic.Common.Services;

namespace Game.Assets.Scripts.Game.Logic.Models.Services
{
    public interface IModelServices
    {
        static IModelServices Default { get; set; }
        T Add<T>(T service) where T : IService;
        void Remove(IService service);
        void Remove<T>() where T : IService;
    }
}
