﻿using Assets.Scripts.Models.Events;
using System;
using Tests.Assets.Scripts.Game.Logic.Models.Events;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Common
{
    public class AutoUpdatedProperty<TValue, TEvent> where TEvent : IGameEvent
    {
        private Func<TEvent, TValue> _setter;
        private HistoryReader _history;
        private TValue _value;

        public AutoUpdatedProperty(History history, Func<TEvent, TValue> setter)
        {
            _setter = setter;
            _history = new HistoryReader(history);
            _history.Subscribe<TEvent>(OnEvent).Update();
        }

        public TValue Value { get => GetValue(); }

        private TValue GetValue()
        {
            _history.Update();
            return _value;
        }

        private void OnEvent(TEvent obj)
        {
            _value = _setter(obj);
        }

    }
}
