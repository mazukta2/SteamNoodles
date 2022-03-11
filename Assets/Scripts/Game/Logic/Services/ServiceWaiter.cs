using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Services
{
    public class ServiceWaiter<T> : Disposable where T : IService
    {
        private Services _services;
        private Action<T> _action;

        public ServiceWaiter(Services services)
        {
            _services = services;
            _services.OnServiceAdded += HandleOnServiceAdded;
        }

        protected override void DisposeInner()
        {
            _services.OnServiceAdded -= HandleOnServiceAdded;
        }

        public ServiceWaiter<T> Await(Action<T> action)
        {
            _action = action;
            Check();
            return this;
        }

        private void HandleOnServiceAdded(IService service)
        {
            Check();
        }

        private void Check()
        {
            var service = _services.Get<T>();
            if (service != null)
            {
                _action(service);
                Dispose();
            }
        }
    }
}
