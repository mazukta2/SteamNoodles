using System;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.DataObjects
{
    public interface IDataProviderService<T> : IService where T : struct, IData
    {
        public IDataProvider<T> Get();
    }
}