using UnityEngine;
using System.Collections;
using Cinemachine;
using System;
using System.Linq;
using Assets.Scripts.Views.Cameras;
using Assets.Scripts.Views.Cameras.Controllers;

namespace Ui.Cameras.Controllers
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public abstract class CentredGameCamera2dController : GameCameraController
    {
        [SerializeField] float _minDistance = 10;
        [SerializeField] float _maxDistance = 50;

        [SerializeField] Collider2D[] _bounds;

        private CinemachineVirtualCamera _camera;
        private Vector2? _originalPosition;
        private const float MaxOutsideBoundsDistance = 3f;

        protected void Start()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
            _camera.enabled = false;
            MainCameraController.Instance.RequisterCamera(this);
        }

        protected virtual void OnDestroy()
        {
            if (MainCameraController.Instance != null)
                MainCameraController.Instance.UnrequisterCamera(this);
        }

        public void SetTemporaryPosition(Vector2 position)
        {
            StopAllCoroutines();
            var oldPosition = GetPosition();
            SetPosition(position, 0.2f);
            _originalPosition = oldPosition;
        }

        public void ResetTemporaryPosition()
        {
            if (_originalPosition != null)
            {
                StopAllCoroutines();
                SetPosition(_originalPosition.Value, 0.2f);
                _originalPosition = null;
            }
        }

        public void MovePosition(Vector2 delta)
        {
            var followPosition = new Vector2(_camera.Follow.position.x, _camera.Follow.position.y);
            SetPosition(followPosition + delta);
            _originalPosition = null;
        }

        public void SetPosition(Vector2 position)
        {
            _originalPosition = null;
            _camera.Follow.position = position;
            StopAllCoroutines();
        }

        public void SetPosition(Vector2 position, float time)
        {
            StopAllCoroutines();
            _originalPosition = null;
            StartCoroutine(MoveToPosition(position, time));
        }

        public Vector2 GetPosition()
        {
            return _camera.Follow.position;
        }

        public void MoveZoom(float delta)
        {
            StopAllCoroutines();
            var transposer = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
            SetZoomDistance(transposer.m_CameraDistance + delta);
        }

        public void SetZoom(float value)
        {
            StopAllCoroutines();
            SetZoomDistance(value);
        }

        public void SetZoom(float value, float time)
        {
            StartCoroutine(ZoomToPosition(value, time));
        }
            
        public float GetZoom()
        {
            var transposer = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
            return transposer != null ? transposer.m_CameraDistance : -1;
        }

        public override void SetEnable(bool value)
        {
            if (value)
                transform.position = MainCameraController.Instance.GetCameraPosition();

            _camera.enabled = value;
        }

        protected void Update()
        {
            if (_camera.enabled)
            {
                UpdateBounds();
                _camera.m_Lens.OrthographicSize = -transform.position.z;
            }
        }

        private void UpdateBounds()
        {
            if (_bounds != null && _bounds.Length > 0)
            {
                if (_bounds.Any(b => b.bounds.Contains(_camera.Follow.position)))
                    return;

                var targetPoint = _bounds.Where(b => !b.bounds.Contains(_camera.Follow.position))
                    .Select(x => x.ClosestPoint(_camera.Follow.position))
                    .OrderBy(x => Vector3.Distance(_camera.Follow.position, x))
                    .First();

                var distance = Vector3.Distance(_camera.Follow.position, targetPoint);
                float speed = distance / MaxOutsideBoundsDistance;

                var followPosition = new Vector2(_camera.Follow.position.x, _camera.Follow.position.y);
                var shift = (targetPoint - followPosition).normalized * speed;

                _camera.Follow.position += new Vector3(shift.x, shift.y, 0);
            }
        }

        private IEnumerator MoveToPosition(Vector3 position, float time)
        {
            var startPosition = _camera.Follow.position;

            for (var i = 0f; i < time; i += Time.deltaTime)
            {
                _camera.Follow.position = Vector3.Lerp(startPosition, position, i / time);
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator ZoomToPosition(float zoom, float time)
        {
            var transposer = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
            var targetZoom = Mathf.Clamp(zoom, _minDistance, _maxDistance);

            var startZoom = transposer.m_CameraDistance;

            for (var i = 0f; i < time; i += Time.deltaTime)
            {
                SetZoomDistance(Mathf.Lerp(startZoom, targetZoom, i / time));
                yield return new WaitForEndOfFrame();
            }
        }

        private void SetZoomDistance(float distance)
        {
            var targetZoom = Mathf.Clamp(distance, _minDistance, _maxDistance);
            var transposer = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
            transposer.m_CameraDistance = targetZoom;
        }
    }
}
