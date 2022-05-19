using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Assets
{
    public class GameControls : Disposable, IGameControls
    {
        private IControls _controls;
        public GameControls(IControls controls)
        {
            (_controls) = (controls);
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
