using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Time
{
    public class GameUpdater
    {
        public event Action OnUpdate = delegate { };

        private GameTime _timeManager;
        private TimeSpan _rate;

        public GameUpdater(GameTime timeManager, TimeSpan rate)
        {
            _timeManager = timeManager;
            _rate = rate;
        }

        public void Destroy()
        {
        }
    }
}
