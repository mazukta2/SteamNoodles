using System;
namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface ISoundTrack
    {
        void SetVolume(float volume);
        float GetVolume();
        void Dispose();
    }
}

