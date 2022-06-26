using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Localization;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;

namespace Game.Assets.Scripts.Game.Logic.DataObjects
{
    public interface IDataProvider<T> where T : class, IData
    {
        event Action OnAdded;
        event Action OnRemoved;
        event Action<IModelEvent> OnEvent;

        T Get();
        bool Has();
    }
}