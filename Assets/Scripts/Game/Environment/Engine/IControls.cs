using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface IControls
    {
        void ShakeCamera();
        event Action OnLevelClick;
        event Action<GameVector3> OnLevelPointerMoved;
        event Action<string> OnTimelineAnimationFinished;
        GameVector3 PointerLevelPosition { get; }
        void ChangeCamera(string name, float time);
        void PlayAnimation(string name, string animationName);
        void PlayTimelineAnimation(string name);
        ISoundTrack CreateTrack(string name);
    }
}
