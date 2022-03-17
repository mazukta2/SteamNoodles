using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Services
{
    public class Services : Disposable
    {
        public Action<IService> OnServiceAdded = delegate { };

        private List<IService> _services = new List<IService>();

        protected override void DisposeInner()
        {
            foreach (var item in _services)
                item.Dispose();
            _services.Clear();
        }

        public ServiceWaiter<T> Request<T>(Action<T> load) where T : IService
        {
            return new ServiceWaiter<T>(this).Await(load);
        }

        public ServiceWaiter<T1, T2> Request<T1, T2>(Action<T1, T2> load) where T1 : IService where T2 : IService
        {
            return new ServiceWaiter<T1, T2>(this).Await(load);
        }

        public T Get<T>() where T : IService
        {
            return _services.OfType<T>().FirstOrDefault();
        }

        public T Add<T>(T service) where T : IService
        {
            _services.Add(service);
            OnServiceAdded(service);
            return service;
        }

        public void Remove<T>(T service) where T : IService
        {
            _services.Remove(service);
        }
    }
}
