using System;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;

namespace Game.Assets.Scripts.Game.Logic.Infrastructure.Music
{
    public class MusicTrack : Disposable
    {
        public string Name { get; }
        private ISoundTrack _sound;

        public MusicTrack(string name, ISoundTrack soundTrack)
        {
            Name = name;
            _sound = soundTrack ?? throw new ArgumentException(nameof(soundTrack));
        }

        protected override void DisposeInner()
        {
            _sound.Dispose();
        }

        public float Volume => _sound.GetVolume();


        public void ChangeVolume(float v)
        {
            _sound.SetVolume(Volume + v);
        }

        public void SetVolume(int v)
        {
            _sound.SetVolume(v);
        }
    }
}

