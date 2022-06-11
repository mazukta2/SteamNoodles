using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Common.Services
{
    public class ServiceManager : Disposable, IPresenterServices, IModelServices
    {
        private List<IService> _list = new List<IService>();
        private RepositoryManager _repository = new RepositoryManager();

        public ServiceManager()
        {
        }

        public T Add<T>(T service) where T : IService
        {
            if (_list.OfType<T>().Any())
                throw new Exception($"Only one instance of {typeof(T).Name} allowed");

            _list.Add(service);

            if (service is IBaseRepository repository)
                _repository.Add(repository);

            return service;
        }

        public void Remove(IService service)
        {
            if (!_list.Contains(service))
                return;

            _list.Remove(service);

            if (service is IBaseRepository repository)
                _repository.Remove(repository);

            if (service is IDisposable disposable)
                disposable.Dispose();
        }

        public void Remove<T>() where T : IService
        {
            foreach (var service in _list.OfType<T>().ToArray())
                Remove(service);
        }

        public T Get<T>() where T : IService
        {
            if (!Has<T>())
                throw new Exception($"No service with name {typeof(T)}");

            return _list.OfType<T>().Last();
        }

        public bool Has<T>() where T : IService
        {
            return _list.OfType<T>().Any();
        }

        protected override void DisposeInner()
        {
            foreach (var service in _list.ToArray())
                Remove(service);
        }

    }
}
