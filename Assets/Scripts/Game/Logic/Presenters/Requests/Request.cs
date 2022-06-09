using Game.Assets.Scripts.Game.Logic.Common.Services.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Requests
{
    public abstract class Request : IRequest
    {
        public bool IsHandled { get; set; }
    }
}
