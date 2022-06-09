using Game.Assets.Scripts.Game.Logic.Common.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Presenters.Requests;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Common.Services.Commands
{
    public class RequestManager : IRequests
    {
        private List<IBaseRequestHandler> _list = new List<IBaseRequestHandler>();

        public void Add(IBaseRequestHandler handler)
        {
            _list.Add(handler);
        }

        public void Remove(IBaseRequestHandler handler)
        {
            _list.Remove(handler);
        }

        public T Send<T>(T request) where T : IRequest
        {
            foreach (var handler in _list)
            {
                if (handler is IRequestHandler<T> commandHandler)
                {
                    commandHandler.Handle(request);
                    request.IsHandled = true;
                }
            }

            if (!request.IsHandled)
                throw new Exception("Request did't handled");

            return request;
        }

        public RequestLink<T> Get<T>() where T : IRequest
        {
            foreach (var handler in _list)
            {
                if (handler is IRequestHandler<T> commandHandler)
                {
                    return new RequestLink<T>(commandHandler);
                }
            }
            throw new Exception("Request did't handled");
        }

    }
}
