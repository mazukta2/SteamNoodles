using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Time
{
    public class GameTime : IGameTime
    {
        public event Action<float, float> OnTimeChanged = delegate { };

        public float Time { get; private set; }

        private const float _pieceSize = 1f;

        public GameTime()
        {
        }

        public void MoveTime(float time)
        {
            if (time < 0)
                throw new Exception("Can't be null");

            var oldTime = 0f;
            while (time > _pieceSize)
            {
                oldTime = Time;
                Time += _pieceSize;
                time -= _pieceSize;
                OnTimeChanged(oldTime, oldTime + _pieceSize);
            }

            oldTime = Time;
            Time += time;
            OnTimeChanged(oldTime, Time);
        }
    }
}
