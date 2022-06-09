using Game.Assets.Scripts.Game.Logic.Common.Services.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Requests
{
    public class RequestLink<TRequest> where TRequest : IRequest
    {
        private IRequestHandler<TRequest> _commandHandler;

        public RequestLink()
        {
        }

        public RequestLink(IRequestHandler<TRequest> commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public TRequest Get(TRequest request)
        {
            if (_commandHandler != null)
                _commandHandler.Handle(request);
            return request;
        }
    }
}
