using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Scripts.Models.Actions
{
    public abstract class GameAction
    {
        protected IMessageBroker Messages => MessageBroker.Default;

        public abstract void Execute();
    }
}
