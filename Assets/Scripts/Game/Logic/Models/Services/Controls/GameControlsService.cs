using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Controls
{
    public class GameControlsService : Disposable, IService
    {
        private IControls _controls;
        public GameControlsService(IControls controls)
        {
            _controls = controls;
            _controls.OnLevelClick += HandleOnLevelClick;
            _controls.OnLevelPointerMoved += HandleOnLevelPointerMoved;
        }

        protected override void DisposeInner()
        {
            _controls.OnLevelClick -= HandleOnLevelClick;
            _controls.OnLevelPointerMoved -= HandleOnLevelPointerMoved;
        }


        public event Action OnLevelClick = delegate { };
        public event Action<GameVector3> OnLevelPointerMoved = delegate { };
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

    }
}
