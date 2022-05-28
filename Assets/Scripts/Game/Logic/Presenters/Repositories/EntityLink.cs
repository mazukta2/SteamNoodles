using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Repositories
{
    public class EntityLink<T> where T : class
    {
        public EntityLink(IPresenterRepository<T> repository, Uid id)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public IPresenterRepository<T> Repository { get; } 
        public Uid Id { get; }

        public PresenterModel<T> CreateModel()
        {
            return new PresenterModel<T>(Repository, Id);
        }
    }
}
