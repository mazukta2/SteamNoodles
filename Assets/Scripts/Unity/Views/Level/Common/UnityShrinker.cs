using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Common
{
    [Serializable]
    public class UnityShrinker : MonoBehaviour, IFloat
    {
        [SerializeField] float _speed = 1;
        [SerializeField] float _valueTarget = 1;
        float _internalValueTarget = 1;
        private bool _disableAnimationControl;

        public float Value { get => _internalValueTarget; set => SetValue(value); }

        private void SetValue(float value)
        {
            _internalValueTarget = value;
            _disableAnimationControl = true;
        }

        protected void Update()
        {
            var tagret = _disableAnimationControl ? _internalValueTarget : _valueTarget;
            if (tagret != gameObject.transform.localScale.y)
            {
                var value = Mathf.MoveTowards(gameObject.transform.localScale.y, tagret, Time.deltaTime * _speed);
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, value, gameObject.transform.localScale.z);
            }
        }
    }
}