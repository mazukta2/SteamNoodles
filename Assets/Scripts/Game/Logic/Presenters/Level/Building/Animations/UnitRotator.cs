using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Common
{
    public class UnitRotator : Disposable
    {
        public FloatPoint3D Direction { get => _value; set { _value = value; UpdateValues(); } }

        private readonly IRotator _rotator;
        private IGameTime _time;
        private float _frequency;
        private float _speed;
        private FloatPoint3D _value;
        private float _remainTimeToProcess;

        public UnitRotator(IRotator rotator, IGameTime time, float frequency, float speed)
        {
            _rotator = rotator;
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
            _rotator.LookAtDirection(Direction);
        }

        private void HandleTimeChanged(float oldTime, float newTime)
        {
            var timeToProcess = _remainTimeToProcess + newTime - oldTime;
            do
            {
                timeToProcess -= _frequency;
                if (timeToProcess < 0) timeToProcess = 0;
                _rotator.LookAtDirection(Move(_rotator.GetDirection(), Direction));
            }
            while (timeToProcess > _frequency);

            _remainTimeToProcess = timeToProcess;
        }

        private FloatPoint3D Move(FloatPoint3D current, FloatPoint3D target)
        {
            return current.MoveTowards(target, _speed);
        }

    }
}
