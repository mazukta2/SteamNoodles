using System;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Cutscenes.Variations;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Views.Controls;

namespace Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations
{
    public class SwitchCameraStep : CutsceneStep
    {
        private IGameTime _time;
        private IControls _controls;
        private float _delay;
        private string _cameraName;
        private float _startTime;

        public SwitchCameraStep(SwitchCamera definition) : this(definition.Time, definition.CameraName, IGameTime.Default, IGameControls.Default)
        {

        }

        public SwitchCameraStep(float delay, string cameraName, IGameTime time, IControls controls)
        {
            _time = time;
            _controls = controls;
            _delay = delay;
            _cameraName = cameraName;
        }

        protected override void DisposeInner()
        {
            _time.OnTimeChanged -= _time_OnTimeChanged;
        }

        public override void Play()
        {
            _time.OnTimeChanged += _time_OnTimeChanged;
            _startTime = _time.Time;
            _controls.ChangeCamera(_cameraName, _delay);

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

