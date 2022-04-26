﻿using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Unity.Views;
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
        public event Action<FloatPoint> OnLevelPointerMoved = delegate { };
        public event Action<IView> OnPointerEnter = delegate { };
        public event Action<IView> OnPointerExit = delegate { };

        private Vector3 _mousePosition;
        private Plane _plane = new Plane(Vector3.up, 0);
        private float _wheel = 0;

        private List<IView> _highlightedViews = new List<IView>();

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
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }    
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
    }
}
