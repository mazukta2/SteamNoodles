using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Time
{
    public class TimeManager
    {
        public GameTimer MakeTimer(TimeSpan time)
        {
            return new GameTimer(this, time);
        }

        public GameUpdater MakeUpdater(TimeSpan time)
        {
            return new GameUpdater(this, time);
        }
    }
}
