using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Common
{
    [Serializable]
    public class UnityAnimator : IAnimator
    {
        [SerializeField] private Animator _animator;
        private string _currentAnimation;

        public UnityAnimator()
        {
        }

        public UnityAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void Play(string animation, bool startAgain = false)
        {
            if (!startAgain && animation.Equals(_currentAnimation))
                return;

            _currentAnimation = animation;
            _animator.CrossFade(animation, 0.1f);
        }

        public void SwitchTo(string animation)
        {
            if (animation.Equals(_currentAnimation))
                return;

            _currentAnimation = animation;
            _animator.Play(animation, 0, 1);
        }
    }
}