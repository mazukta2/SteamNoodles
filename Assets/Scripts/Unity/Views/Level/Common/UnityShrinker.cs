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

        private float _valueTarget;
        public float Value { get => _valueTarget; set => SetValue(value); }

        private void SetValue(float value)
        {
            _valueTarget = value;
            StopAllCoroutines();
            if (gameObject.activeSelf && gameObject.activeInHierarchy)
                StartCoroutine(MoveToTarget());
        }

        private IEnumerator MoveToTarget()
        {
            while (_valueTarget != gameObject.transform.localScale.y)
            {
                var value = Mathf.MoveTowards(gameObject.transform.localScale.y, _valueTarget, Time.deltaTime * _speed);

                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, value, gameObject.transform.localScale.z);

                yield return null;
            }
        }
    }
}