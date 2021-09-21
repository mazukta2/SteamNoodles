using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Assets.Scripts.Game.Logic.Models.Session
{
    public class SessionRandom
    {
        System.Random _random = new System.Random();

        public int GetRandom(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}
