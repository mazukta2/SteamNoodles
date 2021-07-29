using Assets.Scripts.Views.Cameras;
using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Views.Cameras.Controllers
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public abstract class GameCameraController : MonoBehaviour
    {
        public virtual void SwithToThisCamera(bool inheritZoom = false)
        {
            MainCameraController.Instance.SwithTo(this, inheritZoom);
        }

        public abstract void SetEnable(bool value);
    }
}
