using Assets.Scripts.Views.Cameras;
using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Views.Cameras.Controllers
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public abstract class GameCamera : MonoBehaviour
    {
        private CinemachineVirtualCamera _camera;

        protected void Start()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
            MainCamera.Instance.RequisterCamera(this);
        }

        protected virtual void OnDestroy()
        {
            if (MainCamera.Instance != null)
                MainCamera.Instance.UnrequisterCamera(this);
        }


        public void SwitchOn()
        {
            _camera.Priority = 1;
        }

        public void SwitchOff()
        {
            _camera.Priority = 0;
        }

        public bool IsOn()
        {
            return _camera.Priority > 0; 
        }
    }
}
