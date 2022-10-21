using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Controls;
using System;

namespace Game.Assets.Scripts.Tests.Environment
{
    public class ControlsMock : IControls
    {
        public GameVector3 PointerLevelPosition { get; private set; }

        public event Action OnLevelClick = delegate { };
        public event Action<GameVector3> OnLevelPointerMoved = delegate { };

        public string CurrentCamera { get; set; }

        public void ChangeCamera(string name, float time)
        {
            CurrentCamera = name;
        }

        public void Click()
        {
            OnLevelClick();
        }

        public void MovePointer(GameVector3 floatPoint)
        {
            PointerLevelPosition = floatPoint;
            OnLevelPointerMoved(floatPoint);
        }

        public void ShakeCamera()
        {
        }

        public void PlayAnimation(string name, string animationName)
        {
        }

        public ISoundTrack CreateTrack(string name)
        {
            return new SoundTrackMock(name);
        }
    }
}
