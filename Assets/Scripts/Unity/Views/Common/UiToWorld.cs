using Assets.Scripts.Views.Cameras;
using System.Collections;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Common
{
    public class UiToWorld : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] Canvas _canvas;
        [SerializeField] float _offset;
        void Update()
        {
            var rectTransform = (RectTransform)transform;
            var worldPosition = rectTransform.TransformPoint(rectTransform.rect.center);
            Ray ray = MainCameraController.Instance.GetCamera().ScreenPointToRay(worldPosition);
            _target.transform.position = ray.origin + ray.direction * _offset;
        }
    }
}