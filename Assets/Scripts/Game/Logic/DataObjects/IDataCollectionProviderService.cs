using System;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.DataObjects
{
    public interface IDataCollectionProviderService<T> : IService where T : class, IData
    {
        public IDataCollectionProvider<T> Get();
    }
}