using Assets.Scripts.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.Assets.Scripts.Game.Logic.Models.Events
{
    public class HistoryReader
    {
        private History _history;
        private List<IGameEvent> _localMessages = new List<IGameEvent>();
        private Dictionary<Type, Action<IGameEvent>> _subs = new Dictionary<Type, Action<IGameEvent>>();

        public HistoryReader(History history)
        {
            _history = history;
            _history.Add(this);
        }

        public void Update()
        {
            if (_localMessages.Count < _history.Messages.Count)
            {
                for (int i = _localMessages.Count; i < _history.Messages.Count; i++)
                {
                    var message = _history.Messages[i];
                    _localMessages.Add(message);
                    Resolve(message);
                }
            }
        }

        public HistoryReader Subscribe<T>(Action<T> action) where T : IGameEvent
        {
            _subs[typeof(T)] = (e) => action((T)e);

            for (int i = 0; i < _localMessages.Count; i++)
            {
                var message = _history.Messages[i];
                if (message is T)
                    Resolve(message);
            }

            Update();

            return this;
        }

        private void Resolve(IGameEvent message)
        {
            if (_subs.ContainsKey(message.GetType()))
            {
                _subs[message.GetType()].Invoke(message);
            }
        }

        public void Dispose()
        {
            _history.Remove(this);
        }
    }
}
