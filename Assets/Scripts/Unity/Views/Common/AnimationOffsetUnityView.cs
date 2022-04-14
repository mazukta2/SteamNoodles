using System.Collections;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Common
{
    public class AnimationOffsetUnityView : MonoBehaviour
    {
        public float MinOffset = 0f;
        public float MaxOffset = 1f;

        Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
            animator.SetFloat("Offset", UnityEngine.Random.Range(MinOffset, MaxOffset));
        }
    }
}