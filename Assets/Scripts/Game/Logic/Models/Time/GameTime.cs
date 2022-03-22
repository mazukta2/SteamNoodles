using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Time
{
    public class GameTime 
    {
        public event Action<float> OnTimeChanged = delegate { };

        public float Time { get; private set; }

        private const float _pieces = 1f;

        public GameTime()
        {
        }

        public void MoveTime(float time)
        {
            if (time < 0)
                throw new Exception("Can't be null");

            while (time > _pieces)
            {
                Time += _pieces;
                time -= _pieces;
                OnTimeChanged(_pieces);
            }

            Time += time;
            OnTimeChanged(time);
        }
    }
}
