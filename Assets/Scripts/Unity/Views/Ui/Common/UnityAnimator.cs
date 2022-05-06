using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Common
{
    [Serializable]
    public class UnityAnimator : MonoBehaviour, IAnimator
    {
        public event Action OnFinished = delegate { };
        [SerializeField] private Animator _animator;
        [SerializeField] private float _crossTime = 0.1f; 
        private string _currentAnimation;

        public void Play(string animation, bool startAgain = false)
        {
            if (!startAgain && animation.Equals(_currentAnimation))
                return;

            _currentAnimation = animation;
            StopAllCoroutines();
            StartCoroutine(CrosFadeState(_currentAnimation));
        }

        public void SwitchTo(string animation)
        {
            if (animation.Equals(_currentAnimation))
                return;

            _currentAnimation = animation;
            _animator.Play(animation, 0, 1);
        }

        public IEnumerator CrosFadeState(string name)
        {
            _animator.CrossFade(name, _crossTime);

            while (true)
            {
                var state = _animator.GetCurrentAnimatorStateInfo(0);
                if (state.IsName(name))
                {
                    if (state.loop)
                        yield break;

                    if (state.normalizedTime >= 1)
                    {
                        OnFinished();
                        yield break;
                    }
                }
                yield return null;
            }
        }
    }
}