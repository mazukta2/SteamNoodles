using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Models.Time
{
    public class GameTimer
    {
        public event Action OnFinished = delegate { };

        private GameTime _time;
        private float _endTime;
        private bool _isFinished;

        public GameTimer(GameTime time, float duration)
        {
            if (duration <= 0)
                throw new Exception("Incorrect time");
            _time = time;
            _endTime = time.Time + duration;

            _time.OnTimeChanged += _time_OnTimeChanged;
        }

        private void _time_OnTimeChanged(float obj)
        {
            if (_time.Time >= _endTime)
                Finish();
        }

        private void Finish()
        {
            if (_isFinished)
                return;

            _time.OnTimeChanged -= _time_OnTimeChanged;
            _isFinished = true;
            OnFinished();
        }
    }
}
