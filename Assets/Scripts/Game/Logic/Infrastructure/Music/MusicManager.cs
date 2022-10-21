using System;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Time;

namespace Game.Assets.Scripts.Game.Logic.Infrastructure.Music
{
    public class MusicManager : Disposable
    {
        public const float VolumeChangesSpeed = 0.5f; 

        private MusicTrack _currentMusic;
        private MusicTrack _targetMusic;
        private IGameTime _time;
        private IControls _controls;

        public MusicManager(IControls controls, IGameTime time)
        {
            _time = time;
            _controls = controls;
            _time.OnTimeChanged += _time_OnTimeChanged;
        }

        protected override void DisposeInner()
        {
            if (_currentMusic != null)
                _currentMusic.Dispose();
            if (_targetMusic != null)
                _targetMusic.Dispose();
            _time.OnTimeChanged -= _time_OnTimeChanged;
        }

        private void _time_OnTimeChanged(float oldTime, float newTime)
        {
            if (_currentMusic != null)
            {
                if (_targetMusic == null || _currentMusic != _targetMusic)
                {
                    _currentMusic.ChangeVolume(-VolumeChangesSpeed * (newTime - oldTime));
                    if (_currentMusic.Volume == 0)
                    {
                        _currentMusic.Dispose();
                        _currentMusic = null;

                        if (_targetMusic != null)
                        {
                            _currentMusic = _targetMusic;
                        }
                    }
                }
                else if (_currentMusic.Volume < 1)
                    _currentMusic.ChangeVolume(1);

            }
        }

        public void Start(string name)
        {
            if (_targetMusic != null)
                _targetMusic.Dispose();

            _targetMusic = new MusicTrack(name, _controls.CreateTrack(name));
            _targetMusic.SetVolume(0);

            if (_currentMusic == null)
            {
                _currentMusic = _targetMusic;
                _currentMusic.SetVolume(1);
            }
        }

        public void Stop()
        {
            if (_targetMusic != null && _targetMusic != _currentMusic)
                _targetMusic.Dispose();
            _targetMusic = null;
        }

        public MusicTrack GetCurrent()
        {
            return _currentMusic;
        }

        public MusicTrack GetTarget()
        {
            return _targetMusic;
        }
    }
}

