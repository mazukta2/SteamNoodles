using System;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Cutscenes.Variations;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Views.Controls;

namespace Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations
{
    public class PlayAnimationStep : CutsceneStep
    {
        private IGameTime _time;
        private IControls _controls;
        private float _delay;
        private string _objectName;
        private string _animationName;
        private float _startTime;

        public PlayAnimationStep(PlayAnimation definition) : this(definition.Time,
            definition.ObjectName, definition.AnimationName, IGameTime.Default, IGameControls.Default)
        {

        }

        public PlayAnimationStep(float delay, string objectName, string animationName, IGameTime time, IControls controls)
        {
            _time = time;
            _controls = controls;
            _delay = delay;
            _objectName = objectName;
            _animationName = animationName;
        }

        protected override void DisposeInner()
        {
            _time.OnTimeChanged -= _time_OnTimeChanged;
        }

        public override void Play()
        {
            _time.OnTimeChanged += _time_OnTimeChanged;
            _startTime = _time.Time;
            _controls.PlayAnimation(_objectName, _animationName);

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

