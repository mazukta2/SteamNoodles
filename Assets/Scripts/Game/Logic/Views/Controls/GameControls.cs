using System;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;

namespace Game.Assets.Scripts.Game.Logic.Views.Controls
{
    public class GameControls : Disposable, IGameControls
    {
        private IControls _controls;
        public GameControls(IControls controls)
        {
            (_controls) = (controls);
            _controls.OnLevelClick += HandleOnLevelClick;
            _controls.OnLevelPointerMoved += HandleOnLevelPointerMoved;
            _controls.OnTimelineAnimationFinished += HandleOnOnTimelineAnimationFinished;
        }


        protected override void DisposeInner()
        {
            _controls.OnLevelClick -= HandleOnLevelClick;
            _controls.OnLevelPointerMoved -= HandleOnLevelPointerMoved;
            _controls.OnTimelineAnimationFinished -= HandleOnOnTimelineAnimationFinished;
        }

        public event Action OnLevelClick = delegate { };
        public event Action<GameVector3> OnLevelPointerMoved = delegate { };
        public event Action<string> OnTimelineAnimationFinished;
        public GameVector3 PointerLevelPosition => _controls.PointerLevelPosition;
        public void ShakeCamera() => _controls.ShakeCamera();

        private void HandleOnLevelPointerMoved(GameVector3 obj)
        {
            OnLevelPointerMoved(obj);
        }

        private void HandleOnLevelClick()
        {
            OnLevelClick();
        }

        private void HandleOnOnTimelineAnimationFinished(string obj)
        {
            OnTimelineAnimationFinished(obj);
        }
        
        public void ChangeCamera(string name, float time)
        {
            _controls.ChangeCamera(name, time);
        }

        public void PlayAnimation(string name, string animationName)
        {
            _controls.PlayAnimation(name, animationName);
        }

        public void PlayTimelineAnimation(string name)
        {
            _controls.PlayTimelineAnimation(name);
        }

        public ISoundTrack CreateTrack(string name)
        {
            return _controls.CreateTrack(name);
        }
    }
}
