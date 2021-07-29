using Assets.Scripts.Data;
using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Models.Requests;
using System;
using System.Collections.Generic;
using UniRx;

namespace Assets.Scripts.Models
{
    public class GameContext : IDisposable
    {
        private IMessageBroker _messages;
        private SessionBuildings _buildings;
        private IDisposable _subscription;
        private GameSessionData _gameSessionData;

        private List<object> _contexts = new List<object>();

        public GameContext(IMessageBroker messages, GameSessionData gameSessionData)
        {
            _gameSessionData = gameSessionData;
            _messages = messages;

            _buildings = new SessionBuildings(_gameSessionData.Buildings);
            _messages.Receive<IBuildingsRequest>().Subscribe(Receive);
        }

        public void Dispose()
        {
        }

        protected void Receive(IBuildingsRequest buildings)
        {
            buildings.Buildings = _buildings;
        }
    }
}
