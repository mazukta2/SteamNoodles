using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Time
{
    public class GameUpdater
    {
        public event Action OnUpdate = delegate { };

        private TimeManager _timeManager;
        private TimeSpan _rate;

        public GameUpdater(TimeManager timeManager, TimeSpan rate)
        {
            _timeManager = timeManager;
            _rate = rate;
        }

        public void Destroy()
        {
        }
    }
}
