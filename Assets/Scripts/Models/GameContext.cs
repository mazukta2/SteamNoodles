using Assets.Scripts.Data;
using Assets.Scripts.Models.Events;
using Assets.Scripts.Models.Services;
using Assets.Scripts.Models.Session;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Models
{
    public class GameContext : IDisposable, IServiceManager
    {
        private GameSessionData _gameSessionData;

        private List<IService> _services = new List<IService>();

        public GameContext(GameSessionData gameSessionData)
        {
            _gameSessionData = gameSessionData;
            _services.Add(new SessionService(this, gameSessionData));
        }


        public void Dispose()
        {
        }

        public T Get<T>() where T : class, IService
        {
            return _services.OfType<T>().First();
        }
    }
}
