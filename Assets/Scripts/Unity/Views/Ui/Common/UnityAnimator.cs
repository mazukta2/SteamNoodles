using Game.Assets.Scripts.Game.Logic.Views.Common;
using System.Collections;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Common
{
    public class UnityAnimator : IAnimator
    {
        private Animator _animator;

        public UnityAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void Play(string animation)
        {
            _animator.Play(animation);
        }
    }
}