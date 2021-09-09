using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models.Events
{
    public class History
    {
        public List<IGameEvent> Messages { get; } = new List<IGameEvent>();
        
        public void Add<T>(T evt) where T :IGameEvent
        {
            Messages.Add(evt);
        }
    }
}
