using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Repositories
{
    public class StaticEntityQuery<T> : Disposable, ISingleQuery<T> where T : class, IEntity
    {
        private readonly T _entity;

        public event Action OnAdded = delegate {  };
        public event Action OnRemoved = delegate {  };
        public event Action OnChanged = delegate {  };
        public event Action<IModelEvent> OnEvent = delegate {  };

        public StaticEntityQuery(T entity)
        {
            _entity = entity;
        }

        public T Get()
        {
            return _entity;
        }

        public bool Has()
        {
            return true;
        }
    }
}