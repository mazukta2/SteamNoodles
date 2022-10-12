using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Cutscenes.Variations;
using Game.Assets.Scripts.Game.Logic.Models.Time;

namespace Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations
{
    public class WaitStep : CutsceneStep
    {
        private IGameTime _time;
        private float _delay;
        private float _startTime;

        public WaitStep(Wait definition) : this(definition.Time, IGameTime.Default)
        {

        }

        public WaitStep(float delay, IGameTime time)
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

