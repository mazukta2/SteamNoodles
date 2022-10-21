using System;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;

namespace Game.Assets.Scripts.Game.Logic.Views.Controls
{
    public class SoundTrackMock : Disposable, ISoundTrack
    {
        private float _volume = 1;

        public SoundTrackMock(string name)
        {
        }

        public float GetVolume()
        {
            return _volume;
        }

        public void SetVolume(float volume)
        {
            _volume = volume;
        }
    }
}

