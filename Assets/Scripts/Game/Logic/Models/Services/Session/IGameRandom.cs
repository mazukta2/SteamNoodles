using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Session
{
    public interface IGameRandom
    {
        static IGameRandom Default { get; set; }
        public int GetRandom(int minValue, int maxValue);
        public float GetRandom(float minValue, float maxValue);
        public bool GetRandom();
    }
}
