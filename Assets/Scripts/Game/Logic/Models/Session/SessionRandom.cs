using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Session
{
    public class SessionRandom
    {
        Random _random = new Random();

        public int GetRandom(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}
