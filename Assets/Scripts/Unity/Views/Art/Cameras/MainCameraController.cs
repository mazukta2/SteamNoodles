using Assets.Scripts.Views.Cameras.Controllers;
using Cinemachine;
using GameUnity.Assets.Scripts.Unity.Engine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Views.Cameras
{
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(CinemachineBrain))]
    public class MainCameraController : SingletonMonoBehaviour<MainCameraController>
    {
        private CinemachineBrain _cinemachine;
        private Camera _camera;

        private readonly Plane _plane = new Plane(Vector3.up, Vector3.zero);
        private List<GameCameraController> _cameraControllers = new List<GameCameraController>();
        private GameCameraController _currentCameraController;

        protected override void Awake()
        {
            base.Awake();

            _cinemachine = GetComponent<CinemachineBrain>();
            _camera = GetComponent<Camera>();

            _camera.transparencySortMode = TransparencySortMode.CustomAxis;
            _camera.transparencySortAxis = new Vector3(0, 1, 1);
        }

        public Vector2 GetCameraPosition()
        {
            return _camera.transform.position;
        }

        public GameCameraController GetCurrentController()
        {
            return _currentCameraController;
        }

        public T GetController<T>() where T : GameCameraController
        {
            foreach (var item in _cameraControllers)
            {
                if (item is T)
                    return (T)item;
            }

            return null;
        }

        public void RequisterCamera(GameCameraController controller)
        {
            _cameraControllers.Add(controller);
            if (_currentCameraController == null)
                SwithTo(controller);
        }

        public void UnrequisterCamera(GameCameraController controller)
        {
            _cameraControllers.Remove(controller);
            if (_currentCameraController == controller)
            {
                _currentCameraController = null;
                if (_cameraControllers.Count > 0)
                    SwithTo(_cameraControllers.Last());
            }
        }

        public Camera GetCamera() => _camera;

        public void SwithTo(GameCameraController cameraController)
        {
            if (cameraController == _currentCameraController)
                return;

            if (_currentCameraController != null)
                _currentCameraController.SetEnable(false);
            _currentCameraController = cameraController;
            _currentCameraController.SetEnable(true);
        }

        public void SwithTo<T>() where T : GameCameraController
        {
            SwithTo(GetController<T>());
        }

        public void SwithTo(GameCameraController cameraController, bool inheritZoom = false)
        {
            if (cameraController == _currentCameraController)
                return;

            if (_currentCameraController != null)
            {
                _currentCameraController.SetEnable(false);
            }

            _currentCameraController = cameraController;
            _currentCameraController.SetEnable(true);
        }

        public Vector3 WorldToUI(RectTransform rect, Vector2 worldPosition)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect,
                _camera.WorldToScreenPoint(worldPosition), _camera, out var position);
            return position;
        }

        public Vector3 ScreenToWorld(Vector2 screenPos)
        {
            var pos = _camera.ScreenToWorldPoint(screenPos);
            return new Vector3(pos.x, pos.y, 0);
        }

        //public Vector3 CameraScaleToUi()
        //{
        //    var heighInUnit = Camera.main.orthographicSize * 2;
        //    return Vector2.one * Screen.height * 1 / heighInUnit * DataManager.Settings.WorldToUIScaling;
        //}
    }
}
