using System.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using UnityEngine;

namespace Assets.Scripts.Views.Art
{
    public class TimedAnimator : MonoBehaviour
    {
        [SerializeField] string _name;
        [SerializeField] float _min = 0;
        [SerializeField] float _max = 20;

        Animator _animator;


        void Start()
        {
            _animator = GetComponent<Animator>();
            StartCoroutine(Updater());
        }

        IEnumerator Updater()
        {
            while (true)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(_min, _max));
                _animator.CrossFade(_name, 0.1f);
            }
        }
    }
}