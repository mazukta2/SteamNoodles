using Game.Assets.Scripts.Game.Logic.Common.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Presenters.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Presenters.Commands
{
    public class RequestsMock : IRequests
    {
        public List<IRequest> Requests { get; } = new List<IRequest>();
        private List<IBaseRequestHandler> _list = new List<IBaseRequestHandler>();

        public T Send<T>(T request) where T : IRequest
        {
            Requests.Add(request);
            foreach (var handler in _list)
            {
                if (handler is IRequestHandler<T> commandHandler)
                {
                    commandHandler.Handle(request);
                }
            }
            request.IsHandled = true;
            return request;
        }

        public bool Only<T>()
        {
            if (Requests.Count != 1)
                return false;

            return Requests.First() is T;
        }

        public bool Last<T>()
        {
            if (Requests.Count == 0)
                return false;

            return Requests.Last() is T;
        }

        public bool IsEmpty()
        {
            return Requests.Count == 0;
        }

        public RequestsMock Add(IBaseRequestHandler handler)
        {
            _list.Add(handler);
            return this;
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
            return new RequestLink<T>();
        }

        public static RequestLink<T> GetLink<T>(IRequestHandler<T> commandHandler) where T : IRequest
        {
            return new RequestLink<T>(commandHandler);
        }
    }
}
