using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Assets.Scripts.Game.Logic.Models.Events;

namespace Assets.Scripts.Models.Events
{
    public class History
    {
        private List<WeakReference<HistoryReader>> _readers = new List<WeakReference<HistoryReader>>();

        public List<IGameEvent> Messages { get; } = new List<IGameEvent>();
        
        public void Add<T>(T evt) where T :IGameEvent
        {
            Messages.Add(evt);

            foreach (var reader in _readers.ToList())
            {
                if (reader.TryGetTarget(out var target))
                    target.Update();
                else
                    _readers.Remove(reader);
            }
        }

        public void Add(HistoryReader historyReader)
        {
            _readers.Add(new WeakReference<HistoryReader>(historyReader));
        }

        public void Remove(HistoryReader historyReader)
        {
            foreach (var reader in _readers.ToList())
            {
                if (reader.TryGetTarget(out var target))
                {
                    if (target == historyReader)
                        _readers.Remove(reader);
                }
                else
                {
                    _readers.Remove(reader);
                }
            }
        }
    }
}
