using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Time
{
    public class GameTimer
    {
        private GameTime _timeManager;
        private TimeSpan _time;

        public GameTimer(GameTime timeManager, TimeSpan time)
        {
            _timeManager = timeManager;
            _time = time;
        }

    }
}
