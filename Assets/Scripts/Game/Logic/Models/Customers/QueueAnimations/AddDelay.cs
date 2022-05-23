using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers.Animations
{
    public class AddDelay : BaseSequenceStep
    {
        private IGameTime _time;
        private float _delay;
        private float _startTime;

        public AddDelay(IGameTime time, float delay)
        {
            _time = time;
            _delay = delay;
        }

        protected override void DisposeInner()
        {
            _time.OnTimeChanged -= _time_OnTimeChanged;
        }

        public override void Play()
        {
            _time.OnTimeChanged += _time_OnTimeChanged;
            _startTime = _time.Time;
            if (_delay == 0)
                FireOnFinished();
        }

        private void _time_OnTimeChanged(float oldTime, float newTime)
        {
            if (newTime >= _startTime + _delay)
                FireOnFinished();

        }
    }
}
