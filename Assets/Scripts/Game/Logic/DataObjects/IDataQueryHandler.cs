using System;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.DataObjects
{
    public interface IDataQueryHandler<T> : IService
    {
        public T Get();

        public IDataQuery<T> MakeQuery()
        {
            return new DataQuery<T>(this);
        }
    }
}