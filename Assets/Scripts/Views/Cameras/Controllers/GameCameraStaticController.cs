using UnityEngine;
using Cinemachine;

namespace Assets.Scripts.Views.Cameras.Controllers
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class GameCameraStaticController : GameCameraController
    {
        private CinemachineVirtualCamera _camera;

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

        public override void SetEnable(bool value)
        {
            _camera.enabled = value;
        }
    }
}
