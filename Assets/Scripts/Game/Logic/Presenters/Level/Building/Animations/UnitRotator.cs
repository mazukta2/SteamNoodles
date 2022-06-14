using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Animations
{
    public class UnitRotator : Disposable
    {
        public GameQuaternion Direction { get => _value; set { UpdateValues(value); } }

        private readonly IRotator _rotator;
        private IGameTime _time;
        private float _frequency;
        private float _speed;
        private GameQuaternion _value = GameQuaternion.LookRotation(GameVector3.Forward);
        private float _remainTimeToProcess;

        public UnitRotator(IRotator rotator, IGameTime time, float frequency, float speed)
        {
            _rotator = rotator ?? throw new ArgumentNullException(nameof(rotator));
            _time = time ?? throw new ArgumentNullException(nameof(time));
            _frequency = frequency;
            _speed = speed;

            Skip();
            if (_frequency != 0 && _speed != 0)
                _time.OnTimeChanged += HandleTimeChanged;
        }

        protected override void DisposeInner()
        {
            _time.OnTimeChanged -= HandleTimeChanged;
        }

        private void UpdateValues(GameQuaternion value)
        {
            _value = value;

            if (_frequency == 0 || _speed == 0)
            {
                Skip();
            }
        }

        public void Skip()
        {
            _rotator.Rotation = Direction;
        }

        private void HandleTimeChanged(float oldTime, float newTime)
        {
            var timeToProcess = _remainTimeToProcess + newTime - oldTime;
            do
            {
                timeToProcess -= _frequency;
                if (timeToProcess < 0) timeToProcess = 0;
                _rotator.Rotation = Move(_rotator.Rotation, Direction);
            }
            while (timeToProcess > _frequency);

            _remainTimeToProcess = timeToProcess;
        }

        private GameQuaternion Move(GameQuaternion current, GameQuaternion target)
        {
            return GameQuaternion.RotateTowards(current, target, _speed);
        }

    }
}
