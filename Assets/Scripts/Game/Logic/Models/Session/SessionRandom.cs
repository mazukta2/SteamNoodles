using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Session
{
    public class SessionRandom : IGameRandom
    {
        Random _random = new Random();

        public void SetSeed(int seed)
        {
            _random = new Random(seed);
        }

        public int GetRandom(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }

        public float GetRandom(float minValue, float maxValue)
        {
            return (float)(minValue + (maxValue - minValue) * _random.NextDouble());
        }

        public bool GetRandom()
        {
            return _random.NextDouble() > 0.5f;
        }
    }
}
