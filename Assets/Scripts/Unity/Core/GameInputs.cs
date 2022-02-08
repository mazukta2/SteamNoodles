using Assets.Scripts.Views.Cameras;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Core
{
    public class GameInputs
    {
        private Plane _plane = new Plane(Vector3.up, 0);

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
