using System;
using Game.Assets.Scripts.Game.Logic.Common.Services;

namespace Game.Assets.Scripts.Game.Logic.Repositories
{
    public interface ISingleQuery<out T> : IDisposable, IService where T : class
    {
        event Action OnAdded;
        event Action OnRemoved;
        event Action OnChanged;
        event Action<IModelEvent> OnEvent;
        
        T Get();
        bool Has();
    }
}
