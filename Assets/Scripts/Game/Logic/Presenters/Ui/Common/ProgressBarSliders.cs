using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Common
{
    public class ProgressBarSliders : Disposable
    {
        public float Value { get => _value; set { _value = value; UpdateValues(); } }
        public float Add { get => _add; set { _add = value; UpdateValues(); } }
        public float Remove { get => _remove; set { _remove = value; UpdateValues(); } }

        private IPointsProgressBar _bar;
        private IGameTime _time;
        private float _frequency;
        private float _speed;
        private float _remainTimeToProcess;
        private float _value;
        private float _add;
        private float _remove;

        public ProgressBarSliders(IPointsProgressBar bar, IGameTime time, float frequency, float speed)
        {
            _bar = bar;
            _time = time;
            _frequency = frequency;
            _speed = speed;

            if (_frequency == 0 || _speed == 0)
            {
                Skip();
            }
            else
            {
                _time.OnTimeChanged += HandleTimeChanged;
                HandleTimeChanged(_time.Time, _time.Time);
            }
        }

        protected override void DisposeInner()
        {
            _time.OnTimeChanged -= HandleTimeChanged;
        }

        private void UpdateValues()
        {
            if (_frequency == 0 || _speed == 0)
            {
                Skip();
            }
        }

        public void Skip()
        {
            _bar.MainValue = Value;
            _bar.AddedValue = Add;
            _bar.RemovedValue = Remove;
        }

        private void HandleTimeChanged(float oldTime, float newTime)
        {
            var timeToProcess = _remainTimeToProcess + newTime - oldTime;
            do
            {
                timeToProcess -= _frequency;
                if (timeToProcess < 0) timeToProcess = 0;
                _bar.MainValue = Move(_bar.MainValue, Value);
                _bar.AddedValue = Move(_bar.AddedValue, Add);
                _bar.RemovedValue = Move(_bar.RemovedValue, Remove);
            }
            while (timeToProcess > _frequency);

            _remainTimeToProcess = timeToProcess;
        }

        private float Move(float current, float target)
        {
            if (current <= target)
            {
                if (current + _speed > target)
                    return target;

                return current + _speed;
            }
            else
            {
                if (current - _speed < target)
                    return target;

                return current - _speed;
            }
        }

    }
}
