using Assets.Scripts.Core;
using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Prototypes;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using System.Collections;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Units
{
    public class UnitView : ViewMonoBehaviour, IUnitView
    {
        const float TIME_THRESHOLD = 0.1f;

        [SerializeField] Animator _animator;
        [SerializeField] string _runAnimaionKey;

        private float _lastPositionUpdate;
        private Vector3 _direction;

        public void SetPosition(FloatPoint position)
        {
            var newPosition = position.ToUnityVector(transform.position.y);
            _lastPositionUpdate = Time.time;
            _direction = transform.position - newPosition;
            transform.position = newPosition;
        }

        protected void Update()
        {
            _animator.SetBool(_runAnimaionKey, (Time.time - _lastPositionUpdate < TIME_THRESHOLD));

            if (_direction != Vector3.zero)
                _animator.gameObject.transform.LookAt(_animator.gameObject.transform.position + _direction);
        }
    }
}