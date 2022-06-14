using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Repositories
{
    public interface IPresenterRepository<T> : IService where T : class
    {
        event Action<T> OnAdded;
        event Action<T> OnRemoved;
        event Action<T> OnChanged;
        event Action<T, IModelEvent> OnEvent;
        IReadOnlyCollection<T> Get();
        T Get(Uid uid);
    }
}
