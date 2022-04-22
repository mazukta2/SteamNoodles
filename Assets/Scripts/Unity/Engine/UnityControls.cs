using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Environment.Engine.Controls;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameUnity.Assets.Scripts.Unity.Engine
{
    public class UnityControls : IControls
    {
        public event Action OnLevelClick = delegate { };
        public event Action<FloatPoint> OnLevelPointerMoved = delegate { };
        public event Action<IView> OnPointerEnter = delegate { };
        public event Action<IView> OnPointerExit = delegate { };
        public GameKeysManager Keys { get; } = new GameKeysManager();

        private Vector3 _mousePosition;
        private Plane _plane = new Plane(Vector3.up, 0);
        private float _wheel = 0;
        private List<IView> _oldViews = new List<IView>();
        private List<IView> _newViews = new List<IView>();


        public void Update()
        {
            if (Vector3.Distance(_mousePosition, Input.mousePosition) > 0.01f)
            {
                _mousePosition = Input.mousePosition;
                OnLevelPointerMoved(GetMouseWorldPosition());
            }

            if (IsTapedOnLevel())
            {
                OnLevelClick();
            }

            if (Input.GetKeyDown(KeyCode.R))
                Keys.GetKey(GameKeys.RotateRight).Tap();

            var wheel = Input.GetAxis("Mouse ScrollWheel");
            if (_wheel == 0 || !SameSign(wheel, _wheel))
            {
                if (wheel > 0)
                    Keys.GetKey(GameKeys.RotateRight).Tap();
                if (wheel < 0)
                    Keys.GetKey(GameKeys.RotateLeft).Tap();
            }
            _wheel = wheel;

            var isChanged = false;
            foreach (var oldView in _oldViews)
            {
                if (!_newViews.Contains(oldView))
                {
                    isChanged = true;
                    OnPointerExit(oldView);
                }
            }

            foreach (var newView in _newViews)
            {
                if (!_oldViews.Contains(newView))
                {
                    isChanged = true;
                    OnPointerEnter(newView);
                }
            }

            if (isChanged)
                _oldViews = new List<IView>(_newViews);
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

        public FloatPoint GetMouseWorldPosition()
        {
            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (_plane.Raycast(ray, out distance))
            {
                var point = ray.GetPoint(distance);
                return new FloatPoint(point.x, point.z);
            }
            throw new Exception("Can't reach point");
        }

        public void SetPointerEnter(IView view)
        {
            _newViews.Add(view);
        }

        public void SetPointerExit(IView view)
        {
            _newViews.Remove(view);
        }
    }
}
