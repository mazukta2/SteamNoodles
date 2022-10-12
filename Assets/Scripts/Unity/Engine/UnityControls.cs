using Assets.Scripts.Views.Cameras;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Views.Level.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameUnity.Assets.Scripts.Unity.Engine
{
    public class UnityControls : IControls
    {
        public event Action OnLevelClick = delegate { };
        public event Action<GameVector3> OnLevelPointerMoved = delegate { };

        private Vector3 _mousePosition;
        private Plane _plane = new Plane(Vector3.up, 0);
        private float _wheel = 0;

        public GameVector3 PointerLevelPosition { get; private set; }

        public void Update()
        {
            if (Vector3.Distance(_mousePosition, Input.mousePosition) > 0.01f)
            {
                _mousePosition = Input.mousePosition;
                PointerLevelPosition = GetMouseWorldPosition();
                OnLevelPointerMoved(PointerLevelPosition);
            }

            if (IsTapedOnLevel())
            {
                OnLevelClick();
            }

            if (Input.GetKeyDown(KeyCode.R))
                IGameKeysManager.Default.GetKey(GameKeys.RotateRight).Tap();

            var wheel = Input.GetAxis("Mouse ScrollWheel");
            if (_wheel == 0 || !SameSign(wheel, _wheel))
            {
                if (wheel > 0)
                    IGameKeysManager.Default.GetKey(GameKeys.RotateRight).Tap();
                if (wheel < 0)
                    IGameKeysManager.Default.GetKey(GameKeys.RotateLeft).Tap();
            }
            _wheel = wheel;

            if (Input.GetKeyDown(KeyCode.Escape))
                IGameKeysManager.Default.GetKey(GameKeys.Exit).Tap();
        }

        bool SameSign(float num1, float num2)
        {
            if (num1 > 0 && num2 < 0)
                return false;
            if (num1 < 0 && num2 > 0)
                return false;
            return true;
        }

        private bool IsPointerOverUi()
        {
            if (EventSystem.current == null) return false;

            if (EventSystem.current.IsPointerOverGameObject())
                return true;

            for (int i = 0; i < Input.touchCount; i++)
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId))
                    return true;

            return false;
        }

        public bool IsTapedOnLevel()
        {
            return Input.GetMouseButtonDown(0) && !IsPointerOverUi();
        }

        public GameVector3 GetMouseWorldPosition()
        {
            float distance;
            if (Camera.main == null)
                return GameVector3.Zero;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (_plane.Raycast(ray, out distance))
            {
                var point = ray.GetPoint(distance);
                return new GameVector3(point.x, 0, point.z);
            }
            throw new Exception("Can't reach point");
        }

        public void ShakeCamera()
        {
            UnityShaker.Instance.Shake();
        }

        public void ChangeCamera(string name, float time)
        {
            MainCamera.Instance.SwitchTo(name, time);
        }
    }
}
