using Game.Assets.Scripts.Game.Logic.Views.Common;
using System.Collections;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Common
{
    public class UnityAnimator : IAnimator
    {
        private Animator _animator;
        private string _currentAnimation;


        public UnityAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void Play(string animation, bool startAgain = false)
        {
            if (!startAgain && animation.Equals(_currentAnimation))
                return;

            _currentAnimation = animation;
            _animator.SetTrigger(animation);
        }
    }
}