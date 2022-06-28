﻿using System;
using Game.Assets.Scripts.Game.Logic.Common.Services;

namespace Game.Assets.Scripts.Game.Logic.DataObjects
{
    public interface IDataCollectionProviderService<T> : IService where T : struct, IData
    {
        public IDataCollectionProvider<T> Get();
    }
}