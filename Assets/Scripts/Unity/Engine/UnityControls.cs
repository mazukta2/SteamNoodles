using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Services.Controls;
using GameUnity.Assets.Scripts.Unity.Views.Level.Common;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameUnity.Assets.Scripts.Unity.Engine
{
    public class UnityControls : IControls
    {
        public event Action OnLevelClick = delegate { };
        public event Action<GameKeys> OnTap = delegate { };
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
                OnTap(GameKeys.RotateRight);

            var wheel = Input.GetAxis("Mouse ScrollWheel");
            if (_wheel == 0 || !SameSign(wheel, _wheel))
            {
                if (wheel > 0)
                    OnTap(GameKeys.RotateRight);
                if (wheel < 0)
                    OnTap(GameKeys.RotateLeft);
            }
            _wheel = wheel;

            if (Input.GetKeyDown(KeyCode.Escape))
                OnTap(GameKeys.Exit);
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
    }
}
