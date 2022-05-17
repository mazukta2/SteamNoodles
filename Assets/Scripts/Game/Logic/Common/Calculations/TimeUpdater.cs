using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Common.Calculations
{
    public class TimeUpdater : Disposable
    {
        public event Action OnUpdate = delegate { };

        private IGameTime _time;
        private float _frequency;
        private float _remainTimeToProcess;

        public TimeUpdater(IGameTime time, float frequency = 0.2f)
        {
            _time = time;
            _frequency = frequency;
        }

        protected override void DisposeInner()
        {
            _time.OnTimeChanged -= HandleTimeChanged;
        }

        public void Start()
        {
            if (_frequency > 0)
                _time.OnTimeChanged += HandleTimeChanged;
        }

        private void HandleTimeChanged(float oldTime, float newTime)
        {
            _remainTimeToProcess += newTime - oldTime;
            while (_remainTimeToProcess >= _frequency)
            {
                _remainTimeToProcess -= _frequency;
                OnUpdate();
            }
        }
    }
}
