using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;

namespace Game.Assets.Scripts.Game.Logic.Services.Controls
{
    public class GameControlsService : Disposable, IService
    {
        private Dictionary<GameKeys, KeyCommand> _keys = new Dictionary<GameKeys, KeyCommand>();
        private IControls _controls;

        public GameControlsService(IControls controls)
        {
            _controls = controls;
            _controls.OnLevelClick += HandleOnLevelClick;
            _controls.OnLevelPointerMoved += HandleOnLevelPointerMoved;
            _controls.OnTap += HandleOnTap;

            foreach (GameKeys key in Enum.GetValues(typeof(GameKeys)))
            {
                _keys.Add(key, new KeyCommand(key));
            }
        }

        protected override void DisposeInner()
        {
            _controls.OnTap -= HandleOnTap;
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

        public KeyCommand GetKey(GameKeys key)
        {
            return _keys[key];
        }

        public void TapKey(GameKeys key)
        {
            GetKey(key).Tap();
        }

        private void HandleOnTap(GameKeys key)
        {
            GetKey(key).Tap();
        }
    }
}
